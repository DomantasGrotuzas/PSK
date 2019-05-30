using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
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
        private readonly IHostingEnvironment _env;

        private readonly ITripEmployeeDataAccess _tripEmployeeDataAccess;

        private readonly IEmployeeService _employeeService;

        private readonly IAccommodationService _accommodationService;

        private readonly IDataAccess<Accommodation> _accommodationDataAccess;

        private readonly IDataAccess<Trip> _tripDataAccess;

        private readonly IMapper _mapper;

        private readonly IFileDataAccess _fileDataAccess;

        public TripEmployeeController(IMapper mapper, ITripEmployeeDataAccess tripEmployeeDataAccess,
            IEmployeeService employeeService, IAccommodationService accommodationService, 
            IDataAccess<Accommodation> accommodationDataAccess, IDataAccess<Trip> tripDataAccess,
            IFileDataAccess fileDataAccess, IHostingEnvironment env)
        {
            _mapper = mapper;
            _tripEmployeeDataAccess = tripEmployeeDataAccess;
            _employeeService = employeeService;
            _accommodationService = accommodationService;
            _accommodationDataAccess = accommodationDataAccess;
            _tripDataAccess = tripDataAccess;
            _fileDataAccess = fileDataAccess;
            _env = env;
        }

        [Authorize]
        public async Task<IActionResult> TripEmployees(Guid tripId)
        {
            return View(await _tripEmployeeDataAccess.GetAll(tripId));
        }

        [Authorize(Roles = "Organizer")]
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
            tripEmployee.Files = new FormFileCollection();
            return View(tripEmployee);
        }

        [Authorize(Roles = "Organizer")]
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
            var createdTripEmployee = await _tripEmployeeDataAccess.Add(tripEmployee);

            string path = Path.Combine(_env.WebRootPath, "Attachments", "TripEmployee", createdTripEmployee.Id.ToString());

            if(tripEmployeeDto.Files != null)
                foreach (IFormFile formFile in tripEmployeeDto.Files)
                {
                    await _fileDataAccess.Add(formFile, path, tripEmployee);
                }

            return Redirect($"tripEmployees?tripId={tripEmployeeDto.Trip.Id}");
        }

        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var tripId = (await _tripEmployeeDataAccess.Get(id)).Trip.Id;
            await _tripEmployeeDataAccess.Remove(id);
            return Redirect($"/tripEmployee/tripEmployees?tripId={tripId}");
        }

        [Authorize(Roles = "Organizer")]
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

            var acommodationThatIsSelected = tripEmployeeDto.AvailableAccommodations
                .FirstOrDefault(x => x.Id == tripEmployee.AccommodationReservation?.Accommodation?.Id);
            if (acommodationThatIsSelected?.SpacesAvailable != null)
            {
                acommodationThatIsSelected.SpacesAvailable++;
            }

            return View(tripEmployeeDto);
        }

        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Update(TripEmployeeDto tripEmployeeDto)
        {
            var tripEmployee = _mapper.Map<TripEmployee>(tripEmployeeDto);
            //tripEmployee.EmployeeId = Guid.Parse(tripEmployeeDto.EmployeeId);

            var existingTripEmployee = await _tripEmployeeDataAccess.Get(tripEmployee.Id);
            existingTripEmployee.EmployeeId = Guid.Parse(tripEmployeeDto.EmployeeId);
            existingTripEmployee.Comment = tripEmployee.Comment;
            existingTripEmployee.CarReservationStatus = tripEmployee.CarReservationStatus;
            existingTripEmployee.CarReservationPrice = tripEmployee.CarReservationPrice;
            existingTripEmployee.PlaneTicketStatus = tripEmployee.PlaneTicketStatus;
            existingTripEmployee.PlaneTicketPrice = tripEmployee.PlaneTicketPrice;          


            if (tripEmployeeDto.AccommodationId != null && Guid.Parse(tripEmployeeDto.AccommodationId) != Guid.Empty)
            {
                existingTripEmployee.AccommodationReservation.AccommodationId = Guid.Parse(tripEmployeeDto.AccommodationId);
            }
            else
            {
                existingTripEmployee.AccommodationReservation.AccommodationId = null;
            }
            await _tripEmployeeDataAccess.Update(existingTripEmployee);
            return Redirect($"tripEmployees?tripId={tripEmployeeDto.Trip.Id}");
        }
    }
}