using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mifdemo.Domain.Seed;

namespace Mifdemo.Domain.Models
{
    public class VoucherModel : BaseAuditModel
    {
        public string Code { get; set; }
        public DateTime TimeStamp { get; set; }
        public string VoucherType { get; set; }
        public int ClientId { get; set; }
        public ClientModel Client { get; set; }
        public string Reason { get; set; }
        public string LastOpration { get; set; }
        public int Reference { get; set; }
        public double Amount { get; set; }
        public int PurchasedProductId { get; set; }
        public PurchasedProductModel PurchasedProduct { get; set; }

        [Required]
        public int BranchId { get; set; }
        public BranchModel Branch { get; set; }
    }
}