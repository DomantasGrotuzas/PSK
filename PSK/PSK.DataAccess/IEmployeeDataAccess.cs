using System.Collections.Generic;
using System.Threading.Tasks;
using PSK.Domain;

namespace PSK.DataAccess
{
    public interface IEmployeeDataAccess
    {
        Task<IEnumerable<Employee>> GetAll();

        Task<Employee> AddEmployee(Employee employee);

        Task<Employee> GetEmployee(int id);

        Task DeleteEmployee(Employee employee);

        Task UpdateEmployee(Employee employee);
    }
}