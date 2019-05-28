using System;
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
        private readonly IDataAccess<TripEmployee> _dataAccess;

        private readonly IMapper _mapper;

        public TripEmployeeController(IDataAccess<TripEmployee> dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }
    }
}