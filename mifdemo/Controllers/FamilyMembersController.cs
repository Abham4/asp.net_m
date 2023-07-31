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
    public class FamilyMembersController : ControllerBase
    {
        private readonly IFamilyService _db;
        public FamilyMembersController(IFamilyService service)
        {
            _db = service;
        }

        [HttpPost]
        public async Task<ActionResult> PostFamilyAsync(FamilyMembersModel family)
        {
            await _db.PostFamilyAsync(family);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFamilyAsync(int id, FamilyMembersModel family)
        {
            if(id != family.Id)
                return BadRequest();

            await _db.UpdateFamilyAsync(family);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFamilyAsync(int id)
        {
            await _db.DeleteFamilyAsync(id);

            return NoContent();
        }
    }
}
