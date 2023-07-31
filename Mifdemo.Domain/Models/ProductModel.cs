using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mifdemo.Domain.Seed;

namespace Mifdemo.Domain.Models
{
    public class ProductModel : BaseAuditModel
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public float OriginalLoan { get; set; }
        public float LoanBalance { get; set; }
        public float AmountPaid { get; set; }

        [Display(Name = "Product Type")]
        public string ProductType { get; set; }
        public List<ProductPurchasedProduct> PurchasedProducts { get; set; }
    }
}