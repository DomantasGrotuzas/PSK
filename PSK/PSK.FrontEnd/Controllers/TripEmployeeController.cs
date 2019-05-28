using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using PSK.DataAccess.Interfaces;
using PSK.Domain;

namespace PSK.FrontEnd.Controllers
{
    public class TripEmployeeController : Controller
    {
        private readonly ITripEmployeeDataAccess _tripEmployeeDataAccess;

        private readonly IDataAccess<Trip> _tripDataAccess;

        private readonly IMapper _mapper;

        public TripEmployeeController(IMapper mapper, ITripEmployeeDataAccess tripEmployeeDataAccess, IDataAccess<Trip> tripDataAccess)
        {
            _mapper = mapper;
            _tripEmployeeDataAccess = tripEmployeeDataAccess;
            _tripDataAccess = tripDataAccess;
        }

        public async Task<IActionResult> TripEmployees(Guid tripId)
        {
            return View(await _tripEmployeeDataAccess.GetAll(tripId));
        }
    }
}