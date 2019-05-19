using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using PSK.Domain;
using PSK.Domain.Identity;
using PSK.Services;

namespace PSK.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
        {
            var result = _mapper.Map<IEnumerable<EmployeeDto>>(await _employeeService.GetAll());
            return result.ToArray();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> Get(int id)
        {
            return _mapper.Map<EmployeeDto>(await _employeeService.Get(id));
        }

        [HttpPost]
        public async Task Post([FromBody] EmployeeDto employeeDto)
        {
            await _employeeService.Create(_mapper.Map<Employee>(employeeDto));
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] EmployeeDto employeeDto)
        {
            await _employeeService.Update(id, _mapper.Map<Employee>(employeeDto));
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _employeeService.Delete(id);
        }
    }
}