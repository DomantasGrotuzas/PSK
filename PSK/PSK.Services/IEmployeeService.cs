using System.Collections.Generic;
using System.Threading.Tasks;
using PSK.Domain;

namespace PSK.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAll();

        Task<Employee> Get(int id);

        Task<Employee> Create(Employee employee);

        Task<Employee> Update(int id, Employee employee);

        Task Delete(int id);
    }
}