using Mifdemo.Domain.Seed;

namespace Mifdemo.Domain.Models
{
    public class IdentifierModel : BaseAuditModel
    {
        public string DocumentType { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public ClientModel Client { get; set; }
    }
}