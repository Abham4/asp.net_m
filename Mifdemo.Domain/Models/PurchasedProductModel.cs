using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mifdemo.Domain.Seed;

namespace Mifdemo.Domain.Models
{
    public class PurchasedProductModel : BaseAuditModel
    {
        public float OriginalLoan { get; set; }
        public float LoanBalance { get; set; }
        public float AmountPaid { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; }
        public float Rate { get; set; }
        public int AccountId { get; set; }
        public AccountModel Account { get; set; }
        public int PaymentContrat { get; set; }
        
        [NotMapped]
        [Required]
        public DateTime StartingDate { get; set; }
        public List<VoucherModel> Vouchers { get; set; }

        [Required]
        public List<ProductPurchasedProduct> Products { get; set; }
        public List<PaymentScheduleModel> PaymentSchedules { get; set; }
    }
}