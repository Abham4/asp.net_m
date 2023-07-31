using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mifdemo.ViewModels.Authentication;
using Mifdemo.Domain.Interface.ServiceInterface;
using Mifdemo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mifdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _db;
        public ProductsController(IProductService service)
        {
            _db = service;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ProductModel>>> GetallProductsAsync()
        {
            return await _db.GetallProductsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductsAsync(int id)
        {
            var product = await _db.GetProductsAsync(id);
            if (product == null)
                return NotFound();
            else
                return product;
        }

        [HttpPost]
        public async Task<ActionResult> PostProductsAsync(ProductModel product)
        {
            await _db.PostProductsAsync(product);

            return StatusCode(201);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductsAsync(int id, ProductModel product)
        {
            if(id != product.Id)
                return BadRequest();
            
            await _db.UpdateProductsAsync(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductsAsync(int id)
        {
            await _db.DeleteProductsAsync(id);

            return NoContent();
        }
    }
}
