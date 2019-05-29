using System;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Services.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using PSK.Domain.Identity;

namespace PSK.FrontEnd.Controllers
{
    public class TripController : Controller
    {
        private readonly ITripService _tripService;

        private readonly IMapper _mapper;

        private readonly IDataAccess<Office> _officeData;

        private readonly IEmployeeService _employeeService;
        private readonly UserManager<Employee> _userManager;

        public TripController(ITripService tripService, IMapper mapper, IDataAccess<Office> officeData, IEmployeeService employeeService, UserManager<Employee> userManager)
        {
            _tripService = tripService;
            _mapper = mapper;
            _officeData = officeData;
            _employeeService = employeeService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Trips()
        {
            return View(await _tripService.GetAll());
        }

        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Create(TripDto tripDto)
        {
            Trip trip = _mapper.Map<Trip>(tripDto);
            trip.StartLocation = await _officeData.Get(Guid.Parse(tripDto.StartLocationId));
            trip.EndLocation = await _officeData.Get(Guid.Parse(tripDto.EndLocationId));
            trip.OrganizerId = (await _userManager.GetUserAsync(User)).Id;


            await _tripService.Create(trip);
            //trip.Organizer = await _employeeService.Get(Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value));
            //await _tripService.Update(trip.Id, trip);
            return Redirect("trips");
        }

        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> AddNew()
        {
            TripDto trip = new TripDto
            {
                Offices = _mapper.Map<IEnumerable<OfficeDto>>(await _officeData.GetAll()).ToList(),
                AllEmployees = (await _employeeService.GetAll()).Select(e => _mapper.Map<EmployeeDto>(e)).ToList()
            };
            return View(trip);
        }

        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _tripService.Delete(id);
            return await Trips();
        }

        [HttpGet]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var trip = await _tripService.Get(id);

            if (trip == null)
                return NotFound();

            return View(trip);
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Update(Trip trip)
        {
            await _tripService.Update(trip.Id, trip);
            return Redirect("trips");
        }
    }
}