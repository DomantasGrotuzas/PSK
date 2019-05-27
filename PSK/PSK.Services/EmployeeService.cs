using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSK.DataAccess;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Domain.Identity;

namespace PSK.Services
{
    public class EmployeeService : IService<Employee>
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

        public async Task<Employee> Get(Guid id)
        {
            return await _employeeData.GetEmployee(id);
        }

        public async Task<Employee> Create(Employee employee)
        {
            return await _employeeData.AddEmployee(employee);
        }

        public async Task<Employee> Update(Guid id, Employee employee)
        {
            var existingEmployee = await _employeeData.GetEmployee(id);
            existingEmployee.Email = employee.Email;
            existingEmployee.Name = employee.Name;
            existingEmployee.Surname = employee.Surname;
            existingEmployee.UserName = employee.UserName;
            existingEmployee.PhoneNumber = employee.PhoneNumber;

            await _employeeData.UpdateEmployee(existingEmployee);

            return employee;
        }

        public async Task Delete(Guid id)
        {
            var employeeToDelete = await _employeeData.GetEmployee(id);
            await _employeeData.DeleteEmployee(employeeToDelete);
        }
    }
}