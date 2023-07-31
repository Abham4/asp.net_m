using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mifdemo.Domain.Seed;

namespace Mifdemo.Domain.Models
{
    public class AccountModel : BaseAuditModel
    {
        public string AccountNo { get; set; }

        [Display(Name = "Account Type")]
        public string AccountType { get; set; }
        public string Status { get; set; }
        public DateTime? AprovedDate { get; set; }
        public int ClientId { get; set; }
        public ClientModel Clients { get; set; }
        public List<PurchasedProductModel> PurchasedProducts { get; set; }
    }
}
