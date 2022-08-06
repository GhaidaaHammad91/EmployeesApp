using ADProject.Interface;
using ADProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProject.Service
{
    public class EmployeesService
    {
        private readonly IRepository<Employee> _employee;

        public EmployeesService(IRepository<Employee> employee)
        {
            _employee = employee;
        }

        public List<Employee> GetEmployees()
        {
            try
            {
                return _employee.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Employee GetEmployee(int employeeId)
        {
            try
            {
                return _employee.GetById(employeeId);
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public async Task<Employee> AddEmployee(Employee employee)
        {
            return await _employee.Create(employee);
        }

        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                if (employee != null)
                {
                     _employee.Update(employee);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
       
        public bool DeleteEmployee(int id)
        {

            try
            {
                var employee = _employee.GetById(id);
                if (employee != null)
                {
                    _employee.Delete(employee);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }
    }
}
