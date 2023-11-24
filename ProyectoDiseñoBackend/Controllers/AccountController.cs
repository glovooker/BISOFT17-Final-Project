using Microsoft.AspNetCore.Mvc;
using ProyectoDiseñoBackend.Iterador;
using ProyectoDiseñoBackend.Modelos;
using ProyectoDiseñoBackend.Servicios;

namespace ProyectoDiseñoBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService) =>
            _accountService = accountService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var account = await _accountService.GetAsync();
            var accountCollection = new GenericCollection<Account>(account);
            var iterator = accountCollection.CreateIterator();

            var accountList = new List<Account>();
            while (iterator.HasNext())
            {
                accountList.Add(iterator.Next());
            }

            return Ok(accountList);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Account>> Get(string id)
        {
            var account = await _accountService.GetAsync(id);

            if (account is null)
            {
                return NotFound();
            }

            return account;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Account newAccount)
        {
            await _accountService.CreateAsync(newAccount);
            return CreatedAtAction(nameof(Get), new { id = newAccount.Id }, newAccount);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Account updatedAccount)
        {
            var account = await _accountService.GetAsync(id);

            if (account is null)
            {
                return NotFound();
            }

            updatedAccount.Id = account.Id;
            await _accountService.UpdateAsync(id, updatedAccount);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var account = await _accountService.GetAsync(id);

            if (account is null)
            {
                return NotFound();
            }

            await _accountService.RemoveAsync(id);
            return NoContent();
        }
    }
}
