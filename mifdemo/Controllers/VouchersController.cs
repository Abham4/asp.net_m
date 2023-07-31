using System.Collections.Generic;
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
    // [Authorize(Roles = UserRoles.Admin)]
    public class VouchersController : ControllerBase
    {
        private readonly IVoucherService _service;
        public VouchersController(IVoucherService voucherService)
        {
            _service = voucherService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VoucherModel>> GetVoucherAsync(int id)
        {
            var voucher = await _service.GetVoucherAsync(id);
            if (voucher == null)
                return NotFound();
            else
                return voucher;
        }    

        [HttpPost]
        public async Task<ActionResult> PostVoucherAsync(VoucherModel voucher)
        {
            await _service.PostVoucherAsync(voucher);

            return StatusCode(201);
        }

        [HttpGet("GetVouchersListByClient/{clientId}")]
        public async Task<List<VoucherModel>> GetVouchersListByClient(int clientId)
        {
            return await _service.GetVouchersListByClient(clientId);
        }
    }
}