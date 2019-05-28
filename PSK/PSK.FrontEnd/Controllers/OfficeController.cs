using System;
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
        private readonly IDataAccess<Office> _officeDataAccess;

        private readonly IMapper _mapper;

        public OfficeController(IDataAccess<Office> officeDataAccess, IMapper mapper)
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
            return Redirect("offices");
        }

        public IActionResult AddNew()
        {
            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _officeDataAccess.Remove(id);
            return Redirect("/office/offices");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var office = await _officeDataAccess.Get(id);

            if (office == null)
                return NotFound();

            return View(office);
        }

        public async Task<IActionResult> Update(Office office)
        {
            await _officeDataAccess.Update(office);
            return Redirect("offices");
        }
    }
}