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

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var entityEntry = await _context.Employees.AddAsync(employee);
            return entityEntry.Entity;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee> GetEmployee(string username)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}