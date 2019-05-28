using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Domain.Identity;

namespace PSK.FrontEnd.Controllers
{
    public class AccommodationController : Controller
    {
        private readonly IDataAccess<Accommodation> _accommodationDataAccess;

        private readonly IMapper _mapper;

        public AccommodationController(IDataAccess<Accommodation> accommodationDataAccess, IMapper mapper)
        {
            _accommodationDataAccess = accommodationDataAccess;
            _mapper = mapper;
        }

        public async Task<IActionResult> Accommodations()
        {
            return View(_mapper.Map<IEnumerable<AccommodationDto>>(await _accommodationDataAccess.GetAll()));
        }

        public async Task<IActionResult> Create(AccommodationDto accommodationDto)
        {
            await _accommodationDataAccess.Add(_mapper.Map<Accommodation>(accommodationDto));
            return Redirect("offices");
        }

        public IActionResult AddNew()
        {
            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _accommodationDataAccess.Remove(id);
            return Redirect("/accommodation/accommodations");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var accommodattion = await _accommodationDataAccess.Get(id);

            if (accommodattion == null)
                return NotFound();

            return View(accommodattion);
        }

        public async Task<IActionResult> Update(Accommodation accommodation)
        {
            await _accommodationDataAccess.Update(accommodation);
            return Redirect("accommodations");
        }
    }
}
