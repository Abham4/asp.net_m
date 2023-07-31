using System.ComponentModel.DataAnnotations;
using Mifdemo.Domain.Seed;

namespace Mifdemo.Domain.Models
{
    public class AddressModel : BaseAuditModel
    {
        [Display(Name = "Address Type")]
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string AddressLine3 { get; set; } = string.Empty;

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;
        public string City { get; set; }

        [Display(Name = "State/Province")]
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public int ClientId { get; set; }
        public ClientModel Client { get; set; }
    }
}