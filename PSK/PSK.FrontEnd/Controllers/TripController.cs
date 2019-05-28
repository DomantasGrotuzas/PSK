using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSK.DataAccess.Interfaces;
using PSK.Domain;

namespace PSK.FrontEnd.Controllers
{
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

        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> Create(Trip trip)
        {
            await _tripData.Add(trip);
            return Redirect("trips");
        }

        [Authorize(Roles = "Organizer,Admin")]
        public IActionResult AddNew()
        {
            return View();
        }

        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _tripData.Remove(id);
            return await Trips();
        }

        [HttpGet]
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var trip = await _tripData.Get(id);

            if (trip == null)
                return NotFound();

            return View(trip);
        }

        [HttpPost]
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> Update(Trip trip)
        {
            await _tripData.Update(trip);
            return Redirect("trips");
        }
    }
}