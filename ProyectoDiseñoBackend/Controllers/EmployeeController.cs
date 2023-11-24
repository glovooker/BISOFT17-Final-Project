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

        [HttpGet("{id:length(24)}")]
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
        public async Task<IActionResult> Post(Employee newEmployee)
        {
            await _employeeService.CreateAsync(newEmployee);
            return CreatedAtAction(nameof(Get), new { id = newEmployee.Id }, newEmployee);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Employee updatedEmployee)
        {
            var employee = await _employeeService.GetAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            updatedEmployee.Id = employee.Id;
            await _employeeService.UpdateAsync(id, updatedEmployee);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
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
