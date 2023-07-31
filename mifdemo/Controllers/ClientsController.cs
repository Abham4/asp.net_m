using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mifdemo.ViewModels.Authentication;
using mifdemo.ViewModels.Clients;
using Mifdemo.Domain.Interface.ServiceInterface;
using Mifdemo.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mifdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _db;
        private readonly IWebHostEnvironment _hostEnv;
        public ClientsController(IClientsService service, IWebHostEnvironment hostEnvironment)
        {
            _db = service;
            _hostEnv = hostEnvironment;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ClientModel>>> GetallClientsAsync()
        {
            return await _db.GetallClientsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientModel>> GetClientsAsync(int id)
        {
            var client = await _db.GetClientsAsync(id);
            if (client == null)
                return NotFound();
            else
                return client;
        }

        [HttpPost]
        public async Task<ActionResult> PostClientsAsync([FromForm] ClientsViewModel clientsView)
        {
            if(clientsView.Img != null)
                clientsView.ProfileImg = await SaveFile(clientsView.Img);
            else
                clientsView.ProfileImg = "No Image";

            var client = new ClientModel()
            {
                FirstName = clientsView.FirstName,
                MiddleName = clientsView.MiddleName,
                LastName = clientsView.LastName,
                PassBookNumber = clientsView.PassBookNumber,
                DOB = clientsView.DOB,
                Gender = clientsView.Gender,
                Address = clientsView.Address,
                Activation_Date = clientsView.Activation_Date,
                IsStaff = clientsView.IsStaff,
                NoOfLoans = clientsView.NoOfLoans,
                LastLoanAmount = clientsView.LastLoanAmount,
                ActiveLoans = clientsView.ActiveLoans,
                ActiveSavings = clientsView.ActiveSavings,
                Status = clientsView.Status,
                PhoneNumber = clientsView.PhoneNumber,
                BranchId = clientsView.BranchId,
                ProfileImg = clientsView.ProfileImg
            };
            
            await _db.PostClientsAsync(client);

            return StatusCode(201);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClientsAsync(int id, [FromForm] ClientsViewModel clientsView)
        {
            if(id <= 0)
                return BadRequest();
            
            if(clientsView.Img != null)
            {
                // var client = await _db.GetClientsAsync(id);
                // DeleteFile(clients.ProfileImg);
                clientsView.ProfileImg = await SaveFile(clientsView.Img);
            }
            else
                clientsView.ProfileImg = "No Image";

            var client = new ClientModel()
            {
                FirstName = clientsView.FirstName,
                MiddleName = clientsView.MiddleName,
                LastName = clientsView.LastName,
                PassBookNumber = clientsView.PassBookNumber,
                DOB = clientsView.DOB,
                Gender = clientsView.Gender,
                Address = clientsView.Address,
                Activation_Date = clientsView.Activation_Date,
                IsStaff = clientsView.IsStaff,
                NoOfLoans = clientsView.NoOfLoans,
                LastLoanAmount = clientsView.LastLoanAmount,
                ActiveLoans = clientsView.ActiveLoans,
                ActiveSavings = clientsView.ActiveSavings,
                Status = clientsView.Status,
                PhoneNumber = clientsView.PhoneNumber,
                BranchId = clientsView.BranchId,
                ProfileImg = clientsView.ProfileImg
            };
            
            await _db.UpdateClientsAsync(client);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClientsAsync(int id)
        {
            var client =  await _db.GetClientsAsync(id);
            DeleteFile(client.ProfileImg);
            await _db.DeleteClientsAsync(id);

            return NoContent();
        }

        [NonAction]
        public async Task<string> SaveFile(IFormFile xFile)
        {
            string xName = new String(Path.GetFileNameWithoutExtension(xFile.FileName)
                .Take(10)
                .ToArray())
                .Replace(' ', '_');
            xName = "Files/Member_Pics/" + xName + '-' + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(xFile.FileName);
            var imagePath = Path.Combine(_hostEnv.ContentRootPath, xName);
            using(var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await xFile.CopyToAsync(fileStream);
            }
            return xName;
        }

        [NonAction]
        public void DeleteFile(string fileName)
        {
            var imagePath = Path.Combine(_hostEnv.ContentRootPath, fileName);

            if(System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
