using System;
using System.Threading.Tasks;
using ADProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ADProject.Interface;
using ADProject.Service;

namespace ADProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //REST Web API standard
        private readonly EmployeesService _employeeService;

        private readonly IRepository<Employee> _employee;

        public EmployeesController(IRepository<Employee> Employee, EmployeesService EmployeeService)
        {
            _employeeService = EmployeeService;
           _employee = Employee;

        }

        [HttpGet()]
        public IActionResult Get()
        {
            var data = _employeeService.GetEmployees();
            var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            return base.Ok(json);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _employeeService.GetEmployee(id);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            return base.Ok(json);
        }

        [HttpPost()]
        public async Task<ActionResult> Post([FromBody] Employee employee)
        {
            try
            {
                await _employeeService.AddEmployee(employee);
                return base.Ok(JsonConvert.SerializeObject(employee));
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
       
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Employee employee)
        {
            try
            {
                _employeeService.UpdateEmployee(employee);
                return base.Ok(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _employeeService.DeleteEmployee(id);
                return base.Ok(true);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
