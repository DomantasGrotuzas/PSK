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
        private readonly ITripDataAccess _tripDataAccess;

        public TripController(ITripService tripService, IMapper mapper,
            IDataAccess<Office> officeData,
            IEmployeeService employeeService,
            UserManager<Employee> userManager, ITripDataAccess tripDataAccess)
        {
            _tripService = tripService;
            _mapper = mapper;
            _officeData = officeData;
            _employeeService = employeeService;
            _userManager = userManager;
            _tripDataAccess = tripDataAccess;
        }

        [Authorize]
        public async Task<IActionResult> Trips()
        {
            return View(await _tripDataAccess.GetAll());
        }

        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Create(TripDto tripDto)
        {
            var trip = _mapper.Map<Trip>(tripDto);
            trip.StartLocation = await _officeData.Get(Guid.Parse(tripDto.StartLocationId));
            trip.EndLocation = await _officeData.Get(Guid.Parse(tripDto.EndLocationId));
            trip.OrganizerId = (await _userManager.GetUserAsync(User)).Id;


            await _tripDataAccess.Add(trip);
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
            await _tripDataAccess.Remove(id);
            return Redirect("/Trip/Trips");
        }

        [HttpGet]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var trip = await _tripDataAccess.Get(id);

            if (trip == null)
                return NotFound();

            return View(trip);
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Update(Trip trip)
        {
            await _tripDataAccess.Update(trip);
            return Redirect("trips");
        }

        [HttpGet]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> MergeSelection(Guid tripId)
        {
            var primaryTrip = await _tripDataAccess.Get(tripId);
            var mergeTripsViewModel = new TripMergeSelectionDto
            {
                PrimaryTrip = primaryTrip,
                Trips = (await _tripDataAccess.GetMergeable(primaryTrip)).ToList()
            };
            return View(mergeTripsViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Merge(Guid primaryTripId, Guid secondaryTripId)
        {
            var dto = new TripMergeDto
            {
                PrimaryTrip = await _tripDataAccess.Get(primaryTripId),
                SecondaryTrip = await _tripDataAccess.Get(secondaryTripId)
            };

            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Organizer, Admin")]
        public async Task<IActionResult> MergeTrips(TripMergeDto dto)
        {
            var primaryTrip = await _tripDataAccess.Get(dto.PrimaryTrip.Id);
            var secondaryTrip = await _tripDataAccess.GetWithEmployees(dto.SecondaryTrip.Id);

            foreach (var tripEmployee in secondaryTrip.Employees)
            {
                tripEmployee.TripId = dto.PrimaryTrip.Id;
            }

            await _tripDataAccess.Remove(dto.SecondaryTrip.Id);

            primaryTrip.OrganizerId = (await _userManager.GetUserAsync(User)).Id;
            primaryTrip.Comment = dto.PrimaryTrip.Comment;
            primaryTrip.StartDate = dto.PrimaryTrip.StartDate;
            primaryTrip.EndDate = dto.PrimaryTrip.EndDate;

            await _tripDataAccess.Update(primaryTrip);

            return Redirect("trips");
        }

        //[Authorize]
        //public async Task<IActionResult> MyTrips()
        //{
        //    return View(await GetMyTripsDto(DateFilter.All));
        //}

        [Authorize]
        public async Task<IActionResult> MyTrips(DateFilter dateFilter)
        {
            return View(await GetMyTripsDto(dateFilter));
        }

        private async Task<MyTripsDto> GetMyTripsDto(DateFilter dateFilter)
        {
            var myTrips = await _tripDataAccess.GetTripsForEmployee((await _userManager.GetUserAsync(User)).Id);
            myTrips = FilterTrips(myTrips, dateFilter);

            IEnumerable<Trip> myOrganizedTrips = null;
            var isOrganizer = User.IsInRole("Organizer");
            if (isOrganizer)
            {
                myOrganizedTrips = await _tripDataAccess.GetTripsForOrganizator((await _userManager.GetUserAsync(User)).Id);
                myOrganizedTrips = FilterTrips(myOrganizedTrips, dateFilter);
            }
            return new MyTripsDto
            {
                MyTrips = myTrips,
                MyOrganizedTrips = myOrganizedTrips,
                DateFilter = dateFilter,
                IsOrganizer = isOrganizer
            };
        }

        private static IEnumerable<Trip> FilterTrips(IEnumerable<Trip> allTrips, DateFilter dateFilter)
        {
            switch (dateFilter)
            {
                case DateFilter.Upcoming:
                    return allTrips.Where(x => x.StartDate > DateTime.Now.Date);
                case DateFilter.Ongoing:
                    return allTrips.Where(x => x.StartDate <= DateTime.Now.Date && x.EndDate >= DateTime.Now.Date);
                case DateFilter.Past:
                    return allTrips.Where(x => x.EndDate < DateTime.Now);
            }
            return allTrips;
        }
    }
}