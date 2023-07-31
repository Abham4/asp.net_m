using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mifdemo.ViewModels.Authentication;
using Mifdemo.Domain.Interface.ServiceInterface;
using Mifdemo.Domain.Models;

namespace mifdemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = UserRoles.Admin)]

    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _db;
        private readonly IWebHostEnvironment _hostEnv;
        public DocumentsController(IDocumentService service, IWebHostEnvironment hostEnvironment)
        {
            _db = service;
            _hostEnv = hostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<List<DocumentModel>>> GetallDocumentAsync()
        {
            return await _db.GetallDocumentAsync();
        }

        [HttpPost]
        public async Task<ActionResult> PostDocumentAsync([FromForm] DocumentModel document)
        {
            document.FileName = await SaveFile(document.File);
            await _db.PostDocumentAsync(document);

            return StatusCode(201);
        }

        [HttpGet("{referId}")]
        public async Task<List<DocumentModel>> GetDocumentsListByRefence(int referId)
        {
            return await _db.GetDocumentsListByRefence(referId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocumentAsync(int id, [FromForm] DocumentModel document)
        {
            if(id != document.Id)
                return BadRequest();

            if(document.File != null)
            {
                // var doc = await _db.GetDocument(id);
                // DeleteFile(document.FileName);
                document.FileName = await SaveFile(document.File);
            }

            await _db.UpdateDocumentAsync(document);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDocumentAsync(int id)
        {
            var doc = await _db.GetDocument(id);
            DeleteFile(doc.FileName);
            await _db.DeleteDocumentAsync(id);

            return NoContent();
        }

        [NonAction]
        public async Task<string> SaveFile(IFormFile xFile)
        {
            string xName = new String(Path.GetFileNameWithoutExtension(xFile.FileName)
                .Take(10)
                .ToArray())
                .Replace(' ', '_');
            xName = "Files/Document_Files/" + xName + '-' + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(xFile.FileName);
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