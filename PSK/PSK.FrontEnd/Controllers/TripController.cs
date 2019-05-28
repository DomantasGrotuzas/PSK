using System;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSK.Domain;
using PSK.Services.Interfaces;

namespace PSK.FrontEnd.Controllers
{
    public class TripController : Controller
    {
        private readonly ITripService _tripService;

        private readonly IMapper _mapper;

        public TripController(ITripService tripService, IMapper mapper)
        {
            _tripService = tripService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Trips()
        {
            return View(await _tripService.GetAll());
        }

        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> Create(TripDto trip)
        {
            await _tripService.Create(_mapper.Map<Trip>(trip));
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
            await _tripService.Delete(id);
            return await Trips();
        }

        [HttpGet]
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var trip = await _tripService.Get(id);

            if (trip == null)
                return NotFound();

            return View(trip);
        }

        [HttpPost]
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> Update(Trip trip)
        {
            await _tripService.Update(trip.Id, trip);
            return Redirect("trips");
        }
    }
}