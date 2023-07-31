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
    [Authorize(Roles = UserRoles.Admin)]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _bs;
        public BranchController(IBranchService branchService)
        {
            _bs = branchService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BranchModel>>> GetBranches()
        {
            return await _bs.GetBranches();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BranchModel>> GetBranch(int id)
        {
            var branch = await _bs.GetBranch(id);
            if (branch == null)
                return NotFound();
            else
                return branch;
        }

        [HttpGet("byname")]
        public async Task<ActionResult<BranchModel>> GetBranchesByName(string name)
        {
            var branch = await _bs.GetBranchesByName(name);
            if (branch == null)
                return NotFound();
            else
                return branch;
        }

        [HttpPost]
        public async Task<ActionResult> PostBranchAsync(BranchModel branch)
        {
            await _bs.PostBranchAsync(branch);

            return StatusCode(201);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBranchAsync(int id, BranchModel branch)
        {
            if(id != branch.Id)
                return BadRequest();

            await _bs.UpdateBranchAsync(branch);

            return NoContent();
        }

        [HttpGet("byaddress/")]
        public async Task<ActionResult<List<BranchModel>>> GetBranchesByAddress(string address)
        {
            return await _bs.GetBranchesByAddress(address);
        }
    }
}