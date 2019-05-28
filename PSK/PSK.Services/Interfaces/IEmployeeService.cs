using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSK.Domain.Identity;

namespace PSK.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> Get(Guid id);
        Task<Employee> Create(Employee employee, ICollection<string> roles);
        Task<Employee> Update(Employee employee, ICollection<string> roles);
        Task Delete(Guid id);
    }
}