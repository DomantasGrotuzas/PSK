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

        private readonly IDataAccess<Office> _officeDataAccess;

        private readonly IMapper _mapper;

        public AccommodationController(IDataAccess<Accommodation> accommodationDataAccess, IMapper mapper, IDataAccess<Office> officeDataAccess)
        {
            _accommodationDataAccess = accommodationDataAccess;
            _mapper = mapper;
            _officeDataAccess = officeDataAccess;
        }

        public async Task<IActionResult> Accommodations()
        {
            return View(_mapper.Map<IEnumerable<AccommodationDto>>(await _accommodationDataAccess.GetAll()));
        }

        public async Task<IActionResult> Create(AccommodationDto accommodationDto)
        {
            var accommodation = _mapper.Map<Accommodation>(accommodationDto);
            accommodation.Office = await _officeDataAccess.Get(Guid.Parse(accommodationDto.OfficeId));
            await _accommodationDataAccess.Add(accommodation);
            return Redirect("accommodations");
        }

        public async Task<IActionResult> AddNew()
        {
            var accommodation = new AccommodationDto
            {
                AllOffices = _mapper.Map<IEnumerable<OfficeDto>>(await _officeDataAccess.GetAll())
            };
            return View(accommodation);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _accommodationDataAccess.Remove(id);
            return Redirect("/accommodation/accommodations");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var accommodation = await _accommodationDataAccess.Get(id);

            if (accommodation == null)
                return NotFound();

            var accommodationDto = _mapper.Map<AccommodationDto>(accommodation);
            accommodationDto.AllOffices = _mapper.Map<IEnumerable<OfficeDto>>(await _officeDataAccess.GetAll());
            return View(accommodationDto);
        }

        public async Task<IActionResult> Update(AccommodationDto accommodationDto)
        {
            var accommodation = _mapper.Map<Accommodation>(accommodationDto);
            accommodation.Office = await _officeDataAccess.Get(Guid.Parse(accommodationDto.OfficeId));
            await _accommodationDataAccess.Update(accommodation);
            return Redirect("accommodations");
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var accommodation = await _accommodationDataAccess.Get(id);
            return View(accommodation);
        }
    }
}
