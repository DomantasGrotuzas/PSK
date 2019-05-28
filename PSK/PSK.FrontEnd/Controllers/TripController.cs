using System;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSK.DataAccess.Interfaces;
using PSK.Domain;

namespace PSK.FrontEnd.Controllers
{
    [Authorize(Roles = "User")]
    public class TripController : Controller
    {
        private readonly IDataAccess<Trip> _tripData;

        private readonly IMapper _mapper;

        public TripController(IDataAccess<Trip> tripData, IMapper mapper)
        {
            _tripData = tripData;
            _mapper = mapper;
        }

        public async Task<IActionResult> Trips()
        {
            return View(await _tripData.GetAll());
        }

        public async Task<IActionResult> Create(TripDto tripDto)
        {
            await _tripData.Add(_mapper.Map<Trip>(tripDto));
            return Redirect("trips");
        }

        public IActionResult AddNew()
        {
            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _tripData.Remove(id);
            return await Trips();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var trip = await _tripData.Get(id);

            if (trip == null)
                return NotFound();

            return View(trip);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Trip trip)
        {
            await _tripData.Update(trip);
            return Redirect("trips");
        }
    }
}