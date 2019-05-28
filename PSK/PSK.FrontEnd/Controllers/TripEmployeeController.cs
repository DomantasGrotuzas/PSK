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
using PSK.Services.Interfaces;

namespace PSK.FrontEnd.Controllers
{
    public class TripEmployeeController : Controller
    {
        private readonly ITripEmployeeDataAccess _tripEmployeeDataAccess;

        private readonly IEmployeeService _employeeService;

        private readonly IMapper _mapper;

        public TripEmployeeController(IMapper mapper, ITripEmployeeDataAccess tripEmployeeDataAccess, IEmployeeService employeeService)
        {
            _mapper = mapper;
            _tripEmployeeDataAccess = tripEmployeeDataAccess;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> TripEmployees(Guid tripId)
        {
            return View(await _tripEmployeeDataAccess.GetAll(tripId));
        }

        public async Task<IActionResult> AddNew()
        {
            var tripEmployee = new TripEmployeeDto
            {
                AllEmployees = _mapper.Map<IEnumerable<EmployeeDto>>(await _employeeService.GetAll())
            };
            return View(tripEmployee);
        }

        public async Task<IActionResult> Create(TripEmployeeDto tripEmployeeDto)
        {
            await _tripEmployeeDataAccess.Add(_mapper.Map<TripEmployee>(tripEmployeeDto));
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

            return View(tripEmployee);
        }
    }
}