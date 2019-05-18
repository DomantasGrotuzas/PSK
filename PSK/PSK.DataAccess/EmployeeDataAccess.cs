using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PSK.Domain;
using PSK.Persistence;

namespace PSK.DataAccess
{
    public class EmployeeDataAccess : IEmployeeDataAccess
    {
        private readonly IDataContext _context;

        public EmployeeDataAccess(IDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var entityEntry = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}