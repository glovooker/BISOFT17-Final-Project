using Microsoft.AspNetCore.Mvc;
using ProyectoDiseñoBackend.Iterador;
using ProyectoDiseñoBackend.Modelos;
using ProyectoDiseñoBackend.Servicios;

namespace ProyectoDiseñoBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingController : ControllerBase
    {
        private readonly BillingService _billingService;

        public BillingController(BillingService billingService) =>
            _billingService = billingService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var billing = await _billingService.GetAsync();
            var billingCollection = new GenericCollection<Billing>(billing);
            var iterator = billingCollection.CreateIterator();

            var billingList = new List<Billing>();
            while (iterator.HasNext())
            {
                billingList.Add(iterator.Next());
            }

            return Ok(billingList);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Billing>> Get(string id)
        {
            var billing = await _billingService.GetAsync(id);

            if (billing is null)
            {
                return NotFound();
            }

            return billing;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Billing newBilling)
        {
            await _billingService.CreateAsync(newBilling);
            return CreatedAtAction(nameof(Get), new { id = newBilling.Id }, newBilling);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Billing updatedBilling)
        {
            var billing = await _billingService.GetAsync(id);

            if (billing is null)
            {
                return NotFound();
            }

            updatedBilling.Id = billing.Id;
            await _billingService.UpdateAsync(id, updatedBilling);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var billing = await _billingService.GetAsync(id);

            if (billing is null)
            {
                return NotFound();
            }

            await _billingService.RemoveAsync(id);
            return NoContent();
        }
    }
}
