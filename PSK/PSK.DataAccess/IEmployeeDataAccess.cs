using System.Threading.Tasks;
using PSK.Domain;

namespace PSK.DataAccess
{
    public interface IEmployeeDataAccess
    {
        Task<Employee> AddEmployee(Employee employee);

        Task<Employee> GetEmployee(int id);

        Task<Employee> GetEmployee(string username);
    }
}