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

        private readonly IMapper _mapper;

        public TripEmployeeController(IMapper mapper, ITripEmployeeDataAccess tripEmployeeDataAccess,
            IEmployeeService employeeService, IAccommodationService accommodationService, 
            IDataAccess<Accommodation> accommodationDataAccess)
        {
            _mapper = mapper;
            _tripEmployeeDataAccess = tripEmployeeDataAccess;
            _employeeService = employeeService;
            _accommodationService = accommodationService;
            _accommodationDataAccess = accommodationDataAccess;
        }

        public async Task<IActionResult> TripEmployees(Guid tripId)
        {
            return View(await _tripEmployeeDataAccess.GetAll(tripId));
        }

        public async Task<IActionResult> AddNew(Guid tripId)
        {
            var tripEmployee = new TripEmployeeDto
            {
                Trip = _mapper.Map<TripDto>(await _tripEmployeeDataAccess.Get(tripId)),
                AllEmployees = _mapper.Map<IEnumerable<EmployeeDto>>(await _employeeService.GetAll()),
                AvailableAccommodations = await _accommodationService.GetAvailableAccommodations(tripId)
            };
            return View(tripEmployee);
        }

        public async Task<IActionResult> Create(TripEmployeeDto tripEmployeeDto)
        {
            var tripEmployee = _mapper.Map<TripEmployee>(tripEmployeeDto);
            tripEmployee.Employee = await _employeeService.Get(Guid.Parse(tripEmployeeDto.EmployeeId));
            if (tripEmployeeDto.AccommodationId != null || Guid.Parse(tripEmployeeDto.AccommodationId) != Guid.Empty)
            {
                tripEmployee.AccommodationReservation.Accommodation =
                    await _accommodationDataAccess.Get(Guid.Parse(tripEmployeeDto.AccommodationId));
            }
            await _tripEmployeeDataAccess.Add(tripEmployee);
            return Redirect("tripEmployees");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _tripEmployeeDataAccess.Remove(id);
            return Redirect("/tripEmployee/tripEmployees");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var tripEmployee = await _tripEmployeeDataAccess.Get(id);

            if (tripEmployee == null)
                return NotFound();

            var tripEmployeeDto = _mapper.Map<TripEmployeeDto>(tripEmployee);

            tripEmployeeDto.AllEmployees = _mapper.Map<IEnumerable<EmployeeDto>>(await _employeeService.GetAll());
            tripEmployeeDto.AvailableAccommodations =
                await _accommodationService.GetAvailableAccommodations(tripEmployee.Trip.Id);

            return View(tripEmployeeDto);
        }

        public async Task<IActionResult> Update(TripEmployeeDto tripEmployeeDto)
        {
            var tripEmployee = _mapper.Map<TripEmployee>(tripEmployeeDto);
            tripEmployee.Employee = await _employeeService.Get(Guid.Parse(tripEmployeeDto.EmployeeId));
            if (tripEmployeeDto.AccommodationId != null || Guid.Parse(tripEmployeeDto.AccommodationId) != Guid.Empty)
            {
                tripEmployee.AccommodationReservation.Accommodation =
                    await _accommodationDataAccess.Get(Guid.Parse(tripEmployeeDto.AccommodationId));
            }
            await _tripEmployeeDataAccess.Update(tripEmployee);
            return Redirect("tripEmployees");
        }
    }
}