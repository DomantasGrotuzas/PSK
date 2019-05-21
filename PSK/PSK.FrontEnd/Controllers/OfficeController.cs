using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Domain.Identity;

namespace PSK.FrontEnd.Controllers
{
    public class OfficeController : Controller
    {
        private readonly IOfficeDataAccess _officeDataAccess;

        private readonly IMapper _mapper;

        public OfficeController(IOfficeDataAccess officeDataAccess, IMapper mapper)
        {
            _officeDataAccess = officeDataAccess;
            _mapper = mapper;
        }

        public async Task<IActionResult> Offices()
        {
            return View(_mapper.Map<IEnumerable<OfficeDto>>(await _officeDataAccess.GetAll()));
        }

        public async Task<IActionResult> Create(OfficeDto officeDto)
        {
            await _officeDataAccess.Add(_mapper.Map<Office>(officeDto));
            return await Offices();
        }

        public IActionResult AddNew()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _officeDataAccess.Remove(id);
            return await Offices();
        }

        //public IActionResult Edit(OfficeDto officeDto)
        //{
        //    return View(officeDto);
        //}

        public async Task<IActionResult> Update(OfficeDto officeDto)
        {
            await _officeDataAccess.Update(_mapper.Map<Office>(officeDto));
            return await Offices();
        }
    }
}