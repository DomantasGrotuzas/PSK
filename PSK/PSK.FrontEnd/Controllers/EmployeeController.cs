using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PSK.Domain.Identity;
using PSK.Services;
using PSK.Services.Interfaces;

namespace PSK.FrontEnd.Controllers
{
    //[Authorize(Roles = "User")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly RoleManager<UserRole> _roleManager;

        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, RoleManager<UserRole> roleManager,IMapper mapper)
        {
            _employeeService = employeeService;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Employees()
        {
            return View(await _employeeService.GetAll());
        }

        public async Task<IActionResult> Create(EmployeeDto employeeDto)
        {
            await _employeeService.Create(_mapper.Map<Employee>(employeeDto), employeeDto.Roles.Select(r => r.Role).ToList());
            return Redirect("employees"); ;
        }

        public IActionResult AddNew()
        {
            return View(new EmployeeDto
            {
                Roles = _roleManager.Roles.Where(r => r.Name.ToLower() != "user").Select(r => new RoleSelection
                {
                    Role = r.Name,
                    IsSelected = false
                }).ToList()
            });
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _employeeService.Delete(id);
            return await Employees();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await _employeeService.Get(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Employee employee)
        {
            await _employeeService.Update(employee.Id, employee);
            return Redirect("employees");
        }
    }
}