using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mifdemo.ViewModels.Authentication;
using Mifdemo.Domain.Interface.ServiceInterface;
using Mifdemo.Domain.Models;

namespace mifdemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoles.Admin)]
    public class IdentifiersController : ControllerBase
    {
        private readonly IIdentifierService _db;
        public IdentifiersController(IIdentifierService service)
        {
            _db = service;
        }

        [HttpPost]
        public async Task<ActionResult> PostIdentifierAsync(IdentifierModel identifier)
        {
            await _db.PostIdentifierAsync(identifier);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateIdentifierAsync(int id, IdentifierModel identifier)
        {
            if(id != identifier.Id)
                return BadRequest();

            await _db.UpdateIdentifierAsync(identifier);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIdentifierAsync(int id)
        {
            await _db.DeleteIdentifierAsync(id);

            return NoContent();
        }
    }
}