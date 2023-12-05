using Microsoft.AspNetCore.Mvc;
using ProyectoDiseñoBackend.Iterador;
using ProyectoDiseñoBackend.Modelos;
using ProyectoDiseñoBackend.Servicios;

namespace ProyectoDiseñoBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService) =>
            _employeeService = employeeService;

        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> Get()
        {
            var employee = await _employeeService.GetAsync();
            var employeeCollection = new GenericCollection<Employee>(employee);
            var iterator = employeeCollection.CreateIterator();

            var employeeList = new List<Employee>();
            while (iterator.HasNext())
            {
                employeeList.Add(iterator.Next());
            }

            return Ok(employeeList);
        }

        [HttpGet]
        [Route("RetrieveById")]
        public async Task<ActionResult<Employee>> Get(string id)
        {
            var employee = await _employeeService.GetAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Post(Employee newEmployee)
        {
            await _employeeService.CreateAsync(newEmployee);
            return CreatedAtAction(nameof(Get), new { id = newEmployee.Id }, newEmployee);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Employee updatedEmployee)
        {
            var employee = await _employeeService.GetAsync(updatedEmployee.Id);

            if (employee is null)
            {
                return NotFound();
            }

            updatedEmployee.Id = employee.Id;
            await _employeeService.UpdateAsync(updatedEmployee.Id, updatedEmployee);
            return NoContent();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var employee = await _employeeService.GetAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            await _employeeService.RemoveAsync(id);
            return NoContent();
        }
    }
}
