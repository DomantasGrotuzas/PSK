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
            return View(await _employeeService.GetAll());
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.Get(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            await _employeeService.Update(employee.Id, employee);
            return Redirect("employees");
        }
    }
}