using Microsoft.AspNetCore.Mvc;
using ProyectoDiseñoBackend.Iterador;
using ProyectoDiseñoBackend.Modelos;
using ProyectoDiseñoBackend.Servicios;

namespace ProyectoDiseñoBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BandController : ControllerBase
    {
        private readonly BandService _bandService;

        public BandController(BandService bandService) =>
            _bandService = bandService;

        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> Get()
        {
            var band = await _bandService.GetAsync();
            var bandCollection = new GenericCollection<Band>(band);
            var iterator = bandCollection.CreateIterator();

            var bandList = new List<Band>();
            while (iterator.HasNext())
            {
                bandList.Add(iterator.Next());
            }

            return Ok(bandList);
        }

        [HttpGet]
        [Route("RetrieveById")]
        public async Task<ActionResult<Band>> Get(string id)
        {
            var band = await _bandService.GetAsync(id);

            if (band is null)
            {
                return NotFound();
            }

            return band;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Post(Band newBand)
        {
            await _bandService.CreateAsync(newBand);
            return CreatedAtAction(nameof(Get), new { id = newBand.Id }, newBand);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Band updatedBand)
        {
            var band = await _bandService.GetAsync(updatedBand.Id);

            if (band is null)
            {
                return NotFound();
            }

            updatedBand.Id = band.Id;
            await _bandService.UpdateAsync(updatedBand.Id, updatedBand);
            return NoContent();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var band = await _bandService.GetAsync(id);

            if (band is null)
            {
                return NotFound();
            }

            await _bandService.RemoveAsync(id);
            return NoContent();
        }
    }
}
