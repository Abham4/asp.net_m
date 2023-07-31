using System;
using Mifdemo.Domain.Seed;

namespace Mifdemo.Domain.Models
{
    public class PaymentScheduleModel : BaseAuditModel
    {
        public DateTime PayingDate { get; set; }
        public DateTime PaidDate { get; set; }
        public double PricipalDue { get; set; }
        public double LoanBalance { get; set; }
        public double Interest { get; set; }
        public double Penality { get; set; }
        public double Paid { get; set; }
        public double Due { get; set; }
        public int ClientId { get; set; }
        public ClientModel Client { get; set; }
        public int PurchasedProductId { get; set; }
        public PurchasedProductModel PurchasedProduct { get; set; }
    }
}