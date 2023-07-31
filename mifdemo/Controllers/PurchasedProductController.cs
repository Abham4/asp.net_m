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
    public class PurchasedProductController : ControllerBase
    {
        private readonly IPurchasedProductService _db;
        public PurchasedProductController(IPurchasedProductService service)
        {
            _db = service;
        }
        [HttpGet]
        public async Task<ActionResult<List<PurchasedProductModel>>> GetallPurchasedProductAsync()
        {
            return await _db.GetallPurchasedProductAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchasedProductModel>> GetPurchasedProductAsync(int id)
        {
            var pp = await _db.GetPurchasedProductAsync(id);
            if (pp == null)
                return NotFound();
            else
                return pp;
        }
        [HttpPost]
        public async Task<ActionResult> PostPurchasedProductAsync(PurchasedProductModel purchasedProduct)
        {
            await _db.PostPurchasedProductAsync(purchasedProduct);

            return StatusCode(201);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePurchasedProductAsync(int id, PurchasedProductModel purchasedProduct)
        {
            if(id != purchasedProduct.Id)
                return BadRequest();

            await _db.UpdatePurchasedProductAsync(purchasedProduct);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePurchasedProductAsync(int id)
        {
            await _db.DeletePurchasedProductAsync(id);

            return NoContent();
        }
    }
}
