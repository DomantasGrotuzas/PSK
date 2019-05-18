using System.Collections.Generic;
using System.Threading.Tasks;
using PSK.DataAccess;
using PSK.Domain;

namespace PSK.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeDataAccess _employeeData;

        public EmployeeService(IEmployeeDataAccess employeeData)
        {
            _employeeData = employeeData;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _employeeData.GetAll();
        }

        public async Task<Employee> Get(int id)
        {
            return await _employeeData.GetEmployee(id);
        }

        public async Task<Employee> Create(Employee employee)
        {
            return await _employeeData.AddEmployee(employee);
        }

        public async Task<Employee> Update(int id, Employee employee)
        {
            await _employeeData.UpdateEmployee(employee);
            return employee;
        }

        public async Task Delete(int id)
        {
            var employeeToDelete = await _employeeData.GetEmployee(id);
            await _employeeData.DeleteEmployee(employeeToDelete);
        }
    }
}