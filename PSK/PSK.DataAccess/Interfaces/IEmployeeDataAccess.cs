using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSK.Domain.Identity;

namespace PSK.DataAccess.Interfaces
{
    public interface IEmployeeDataAccess
    {
        Task<IEnumerable<Employee>> GetAll();

        Task<Employee> AddEmployee(Employee employee);

        Task<Employee> GetEmployee(Guid id);

        Task DeleteEmployee(Employee employee);

        Task UpdateEmployee(Employee employee);
    }
}