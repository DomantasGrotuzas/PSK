using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSK.Domain;
using PSK.Services;

namespace PSK.FrontEnd.Controllers
{
    [Authorize(Roles = "User")]
    public class TripController : Controller
    {
        private readonly IService<Trip> _tripService;

        private readonly IMapper _mapper;

        public TripController(IService<Trip> tripService, IMapper mapper)
        {
            _tripService = tripService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Trips()
        {
            return View(await _tripService.GetAll());
        }

        public async Task<IActionResult> Create(TripDto tripDto)
        {
            await _tripService.Create(_mapper.Map<Trip>(tripDto));
            return Redirect("trips"); ;
        }

        public IActionResult AddNew()
        {
            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _tripService.Delete(id);
            return await Trips();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var trip = await _tripService.Get(id);

            if (trip == null)
                return NotFound();

            return View(trip);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Guid id, Trip trip)
        {
            await _tripService.Update(trip.Id, trip);
            return Redirect("trips");
        }
    }
}