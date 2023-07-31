using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mifdemo.ViewModels.Accounts;
using Mifdemo.Domain.Interface.ServiceInterface;
using Mifdemo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace mifdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _db;
        private readonly Random _rand = new Random();
        public AccountsController(IAccountsService service)
        {
            _db = service;
        }
        [HttpGet]
        public async Task<ActionResult<List<AccountsModel>>> GetallAccountsAsync()
        {
            return await _db.GetallAccountsAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountsModel>> GetAccountsAsync(int id)
        {
            var accs = await _db.GetAccountsAsync(id);
            Console.WriteLine(getImage(accs.Signature));
            return accs;
        }
        [HttpPost]
        public async Task<ActionResult> PostAccountsAsync([FromForm] AccountsViewModel accounts)
        {
            
            AccountsModel acc = new AccountsModel()
            {
                AccountType = accounts.AccountType,
                AccountName = accounts.AccountName,
                AccountUsage = accounts.AccountUsage,
                GlCode = accounts.GlCode,
                Parent = accounts.Parent,
                Tag = accounts.Tag,
                ManualEntriesAllowed = accounts.ManualEntriesAllowed,
                Description = accounts.Description,
                Signature = ToByteArray(accounts.Signature)
            };

            await _db.PostAccountsAsync(acc);

            return StatusCode(201);

        }
        [HttpPut]
        public async Task<ActionResult> UpdateAccountsAsync(AccountsModel accounts)
        {
            await _db.UpdateAccountsAsync(accounts);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccountsAsync(int id)
        {
            await _db.DeleteAccountsAsync(id);

            return NoContent();
        }

        private byte[] ToByteArray(IFormFile file)
        {
            var ms = new MemoryStream();
            file.CopyTo(ms);
            var fileByte = ms.ToArray();
            return fileByte;
        }

        private string getImage(byte[] byteIn)
        {
            var ms = new MemoryStream(byteIn);

            var img = Image.FromStream(ms);

            var rand = _rand.Next(1000, 10000);

            string name = "Signatures/"+rand+".jpg";

            img.Save(name, ImageFormat.Jpeg);

            return name;
        }
    }
}