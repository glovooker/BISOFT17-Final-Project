using Microsoft.AspNetCore.Mvc;
using ProyectoDiseñoBackend.Iterador;
using ProyectoDiseñoBackend.Modelos;
using ProyectoDiseñoBackend.Servicios;

namespace ProyectoDiseñoBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService) =>
            _clientService = clientService;

        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> Get()
        {
            var client = await _clientService.GetAsync();
            var clientCollection = new GenericCollection<Client>(client);
            var iterator = clientCollection.CreateIterator();

            var clientList = new List<Client>();
            while (iterator.HasNext())
            {
                clientList.Add(iterator.Next());
            }

            return Ok(clientList);
        }

        [HttpGet]
        [Route("RetrieveById")]
        public async Task<ActionResult<Client>> Get(string id)
        {
            var client = await _clientService.GetAsync(id);

            if (client is null)
            {
                return NotFound();
            }

            return client;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Post(Client newClient)
        {
            await _clientService.CreateAsync(newClient);
            return CreatedAtAction(nameof(Get), new { id = newClient.Id }, newClient);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Client updatedClient)
        {
            var client = await _clientService.GetAsync(updatedClient.Id);

            if (client is null)
            {
                return NotFound();
            }

            updatedClient.Id = client.Id;
            await _clientService.UpdateAsync(updatedClient.Id, updatedClient);
            return NoContent();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var client = await _clientService.GetAsync(id);

            if (client is null)
            {
                return NotFound();
            }

            await _clientService.RemoveAsync(id);
            return NoContent();
        }
    }
}
