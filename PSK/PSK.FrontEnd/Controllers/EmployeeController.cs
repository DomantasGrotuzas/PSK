using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using PSK.Domain.Identity;
using PSK.Services;

namespace PSK.FrontEnd.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Employees()
        {
            return View(_mapper.Map<IEnumerable<EmployeeDto>>(await _employeeService.GetAll()));
        }

        public async Task<IActionResult> Create(EmployeeDto employeeDto)
        {
            await _employeeService.Create(_mapper.Map<Employee>(employeeDto));
            return await Employees();
        }

        public IActionResult AddNew()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.Delete(id);
            return await Employees();
        }

        public IActionResult Edit(EmployeeDto employeeDto)
        {
            return View(employeeDto);
        }

        public async Task<IActionResult> Update(EmployeeDto employeeDto)
        {
            await _employeeService.Update(employeeDto.Id, _mapper.Map<Employee>(employeeDto));
            return await Employees();
        }
    }
}