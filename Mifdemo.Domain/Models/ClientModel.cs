using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Mifdemo.Domain.Seed;

namespace Mifdemo.Domain.Models
{
    public class ClientModel : BaseAuditModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        
        [Display(Name = "Pass Book Number")]
        public int PassBookNumber { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime? Activation_Date { get; set; }
        public bool IsStaff { get; set; }
        public int NoOfLoans { get; set; }
        public float LastLoanAmount { get; set; }
        public int ActiveLoans { get; set; }
        public float ActiveSavings { get; set; }
        public string Status { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public List<AccountModel> Account { get; set; }
        public List<FamilyMembersModel> Families { get; set; }
        public List<AddressModel> Addresses { get; set; }
        public List<IdentifierModel> Identifiers { get; set; }
        public List<VoucherModel> Vouchers { get; set; }
        public List<PaymentScheduleModel> PaymentSchedules { get; set; }

        [Required]
        public int BranchId { get; set; }
        public BranchModel Branch { get; set; }
        public string ProfileImg { get; set; }

        [NotMapped]
        public IFormFile Img { get; set; }
    }
}
