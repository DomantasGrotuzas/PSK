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
        private readonly RoleManager<UserRole> _roleManager;

        public EmployeeService(UserManager<Employee> userManager, RoleManager<UserRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<Employee> Get(Guid id)
        {
            var employee = await _userManager.FindByIdAsync(id.ToString());
            if (employee == null)
                throw new ArgumentException($"Employee with id: {id}, does't exist");
            return employee;
        }

        public async Task<Employee> Create(Employee employee, ICollection<string> roles)
        {
            var result = await _userManager.CreateAsync(employee);
            Employee createdEmployee;
            if (result.Succeeded)
                createdEmployee = await _userManager.FindByEmailAsync(employee.Email);
            else
                throw new Exception(string.Join('\n', result.Errors.Select(err => $"Error code: {err.Code}.\n\t Description: {err.Description}")));

            await AddRoles(createdEmployee, roles);
            return createdEmployee;

        }

        public async Task<Employee> Update(Employee employee, ICollection<string> roles)
        {
            var existingEmployee = await Get(employee.Id);

            existingEmployee.Email = employee.Email;
            existingEmployee.Name = employee.Name;
            existingEmployee.Surname = employee.Surname;
            existingEmployee.UserName = employee.UserName;
            existingEmployee.PhoneNumber = employee.PhoneNumber;

            await _userManager.UpdateAsync(existingEmployee);

            var currentRoles = await _userManager.GetRolesAsync(existingEmployee);
            await _userManager.RemoveFromRolesAsync(existingEmployee, currentRoles);

            await AddRoles(existingEmployee, roles);

            return employee;
        }

        public async Task Delete(Guid id)
        {
            var employeeToDelete = await Get(id);
            await _userManager.DeleteAsync(employeeToDelete);
        }

        private async Task AddRoles(Employee employee, ICollection<string> roles)
        {
            if (!roles.Any(r => r.ToLower() == "user"))
                roles.Add("user");

            var result = await _userManager.AddToRolesAsync(employee, roles);
            if (!result.Succeeded)
                throw new Exception(string.Join('\n', result.Errors.Select(err => $"Error code: {err.Code}.\n\t Description: {err.Description}")));
        }
    }
}