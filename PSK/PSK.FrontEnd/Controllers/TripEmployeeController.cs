using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using PSK.DataAccess;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Domain.Identity;
using PSK.Services;
using PSK.Services.Interfaces;

namespace PSK.FrontEnd.Controllers
{
    public class TripEmployeeController : Controller
    {
        private readonly ITripEmployeeDataAccess _tripEmployeeDataAccess;

        private readonly IEmployeeService _employeeService;

        private readonly IAccommodationService _accommodationService;

        private readonly IDataAccess<Accommodation> _accommodationDataAccess;

        private readonly IDataAccess<Trip> _tripDataAccess;

        private readonly IMapper _mapper;

        public TripEmployeeController(IMapper mapper, ITripEmployeeDataAccess tripEmployeeDataAccess,
            IEmployeeService employeeService, IAccommodationService accommodationService, 
            IDataAccess<Accommodation> accommodationDataAccess, IDataAccess<Trip> tripDataAccess)
        {
            _mapper = mapper;
            _tripEmployeeDataAccess = tripEmployeeDataAccess;
            _employeeService = employeeService;
            _accommodationService = accommodationService;
            _accommodationDataAccess = accommodationDataAccess;
            _tripDataAccess = tripDataAccess;
        }

        public async Task<IActionResult> TripEmployees(Guid tripId)
        {
            return View(await _tripEmployeeDataAccess.GetAll(tripId));
        }

        public async Task<IActionResult> AddNew(Guid tripId)
        {
            var trip = _mapper.Map<TripDto>(await _tripDataAccess.Get(tripId));
            var availableAccommodations = await _accommodationService.GetAvailableAccommodations(tripId);
            var tripEmployee = new TripEmployeeDto
            {
                Trip = trip,
                AllEmployees = await _employeeService.GetAvailableEmployeesForTrip(tripId),
                AvailableAccommodations = availableAccommodations,
                AccommodationReservation = new AccommodationReservationDto
                {
                    StartDate = trip.StartDate,
                    EndDate = trip.EndDate,
                }
            };
            return View(tripEmployee);
        }

        public async Task<IActionResult> Create(TripEmployeeDto tripEmployeeDto)
        {
            var tripEmployee = _mapper.Map<TripEmployee>(tripEmployeeDto);
            tripEmployee.EmployeeId = Guid.Parse(tripEmployeeDto.EmployeeId);
            tripEmployee.TripId = tripEmployeeDto.Trip.Id;
            tripEmployee.Trip = null;
            if (tripEmployeeDto.AccommodationId != null && Guid.Parse(tripEmployeeDto.AccommodationId) != Guid.Empty)
            {
                tripEmployee.AccommodationReservation.Accommodation =
                    await _accommodationDataAccess.Get(Guid.Parse(tripEmployeeDto.AccommodationId));
            }
            await _tripEmployeeDataAccess.Add(tripEmployee);
            return Redirect($"tripEmployees?tripId={tripEmployeeDto.Trip.Id}");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var tripId = (await _tripEmployeeDataAccess.Get(id)).Trip.Id;
            await _tripEmployeeDataAccess.Remove(id);
            return Redirect($"/tripEmployee/tripEmployees?tripId={tripId}");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var tripEmployee = await _tripEmployeeDataAccess.Get(id);

            if (tripEmployee == null)
                return NotFound();

            var tripEmployeeDto = _mapper.Map<TripEmployeeDto>(tripEmployee);

            var allEmployees = (await _employeeService.GetAvailableEmployeesForTrip(tripEmployee.Trip.Id)).ToList();
            allEmployees.Add(tripEmployeeDto.Employee);
            tripEmployeeDto.AllEmployees = allEmployees;
            tripEmployeeDto.AvailableAccommodations =
                await _accommodationService.GetAvailableAccommodations(tripEmployee.Trip.Id);

            return View(tripEmployeeDto);
        }

        public async Task<IActionResult> Update(TripEmployeeDto tripEmployeeDto)
        {
            var tripEmployee = _mapper.Map<TripEmployee>(tripEmployeeDto);
            tripEmployee.Employee = await _employeeService.Get(Guid.Parse(tripEmployeeDto.EmployeeId));
            if (tripEmployeeDto.AccommodationId != null && Guid.Parse(tripEmployeeDto.AccommodationId) != Guid.Empty)
            {
                tripEmployee.AccommodationReservation.Accommodation =
                    await _accommodationDataAccess.Get(Guid.Parse(tripEmployeeDto.AccommodationId));
            }
            else
            {
                tripEmployee.AccommodationReservation.Accommodation = null;
            }
            await _tripEmployeeDataAccess.Update(tripEmployee);
            return Redirect($"tripEmployees?tripId={tripEmployeeDto.Trip.Id}");
        }
    }
}