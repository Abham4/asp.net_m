using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Mifdemo.Domain.Seed;

namespace Mifdemo.Domain.Models
{
    public class DocumentModel : BaseAuditModel
    {
        public string FileName { get; set; }
        public string Description { get; set; }
        public int ObjectType { get; set; }
        public string DocumentType { get; set; }
        public int Reference { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        [NotMapped]
        public string FileSource { get; set; }
    }
}