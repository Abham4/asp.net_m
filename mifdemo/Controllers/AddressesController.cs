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
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _db;
        public AddressesController(IAddressService service)
        {
            _db = service;
        }

        [HttpPost]
        public async Task<ActionResult> PostAddressAsync(AddressModel address)
        {
            await _db.PostAddressAsync(address);

            return StatusCode(201);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAddressAsync(int id, AddressModel address)
        {
            if(id != address.Id)
                return BadRequest();

            await _db.UpdateAddressAsync(address);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddressAsync(int id)
        {
            await _db.DeleteAddressAsync(id);

            return NoContent();
        }
    }
}
