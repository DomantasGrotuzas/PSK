using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PSK.DataAccess;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Domain.Identity;
using PSK.Services.Interfaces;

namespace PSK.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly UserManager<Employee> _userManager;

        public EmployeeService(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<Employee> Get(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<Employee> Create(Employee employee, ICollection<string> roles)
        {
            var result = await _userManager.CreateAsync(employee);
            Employee createdEmployee;
            if (result.Succeeded)
                createdEmployee = await _userManager.FindByEmailAsync(employee.Email);
            else
                throw new Exception(string.Join('\n', result.Errors.Select(err => $"Error code: {err.Code}.\n\t Description: {err.Description}")));

            if (!roles.Any(r => r.ToLower() == "user"))
                roles.Add("user");

            result = await _userManager.AddToRolesAsync(createdEmployee, roles);
            if (result.Succeeded)
                return createdEmployee;
            throw new Exception(string.Join('\n', result.Errors.Select(err => $"Error code: {err.Code}.\n\t Description: {err.Description}")));
        }

        public async Task<Employee> Update(Guid id, Employee employee)
        {
            var existingEmployee = await Get(id);
            existingEmployee.Email = employee.Email;
            existingEmployee.Name = employee.Name;
            existingEmployee.Surname = employee.Surname;
            existingEmployee.UserName = employee.UserName;
            existingEmployee.PhoneNumber = employee.PhoneNumber;

            await _userManager.UpdateAsync(employee);

            return employee;
        }

        public async Task Delete(Guid id)
        {
            var employeeToDelete = await Get(id);
            await _userManager.DeleteAsync(employeeToDelete);
        }
    }
}