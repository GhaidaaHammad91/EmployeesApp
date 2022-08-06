using ADProject.Data;
using ADProject.Interface;
using ADProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProject.Repository
{
    public class EmployeeRepository : IRepository<Employee>
    {
        EmployeesDBContext _dbContext;
        public EmployeeRepository(EmployeesDBContext employeesDbContext)
        {
            _dbContext = employeesDbContext;
        }
        public async Task<Employee> Create(Employee _object)
        {
            var obj = await _dbContext.Employees.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Delete(Employee _object)
        {
            _dbContext.Employees.Remove(_object);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            try
            {
                return _dbContext.Employees.ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public Employee GetById(int Id)
        {
            return _dbContext.Employees.Where(x => x.Id == Id).FirstOrDefault();
        }

        public void Update(Employee _object)
        {
            _dbContext.Employees.Update(_object);
            _dbContext.SaveChanges();
        }
    }  
  
}
