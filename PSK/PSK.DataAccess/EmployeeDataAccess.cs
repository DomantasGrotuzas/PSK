using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Domain.Identity;
using PSK.Persistence;

namespace PSK.DataAccess
{
    public class EmployeeDataAccess : IEmployeeDataAccess
    {
        private readonly IDataContext _context;
        private readonly UserManager<Employee> _userManager;

        public EmployeeDataAccess(IDataContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _userManager.CreateAsync(employee);
            if (result.Succeeded)
                return await _userManager.FindByEmailAsync(employee.Email);
            throw new Exception(string.Join('\n', result.Errors.Select(err => $"Error code: {err.Code}.\n\t Description: {err.Description}")));
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task DeleteEmployee(Employee employee)
        {
            await _userManager.DeleteAsync(employee);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await _userManager.UpdateAsync(employee);
        }
    }
}