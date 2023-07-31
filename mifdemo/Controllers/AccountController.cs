using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mifdemo.ViewModels.Authentication;
using Mifdemo.Domain.Interface.ServiceInterface;
using Mifdemo.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mifdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _db;
        public AccountController(IAccountService service)
        {
            _db = service;
        }
        [HttpGet]
        public async Task<ActionResult<List<AccountModel>>> GetallAccountAsync()
        {
            return await _db.GetallAccountAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountModel>> GetAccountAsync(int id)
        {
            var acc = await _db.GetAccountAsync(id);
            if (acc == null)
                return NotFound();
            else
                return acc;
        }
        [HttpPost]
        public async Task<ActionResult> PostAccountAsync(AccountModel account)
        {
            await _db.PostAccountAsync(account);

            return StatusCode(201);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAccountAsync(int id, AccountModel account)
        {
            if(id != account.Id)
                return BadRequest();

            await _db.UpdateAccountAsync(account);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccountAsync(int id)
        {
            await _db.DeleteAccountAsync(id);

            return NoContent();
        }
    }
}
