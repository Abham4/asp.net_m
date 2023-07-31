using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace mifdemo.ViewModels.Clients
{
    public class ClientsViewModel
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Display(Name = "Pass Book Number")]
        [Required]
        public int PassBookNumber { get; set; }

        [Display(Name = "Birth Date")]
        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Address { get; set; }
        public DateTime? Activation_Date { get; set; }
        public bool IsStaff { get; set; }
        public int NoOfLoans { get; set; }
        public float LastLoanAmount { get; set; }
        public int ActiveLoans { get; set; }
        public float ActiveSavings { get; set; }

        [Required]
        public string Status { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public int BranchId { get; set; }
        public string ProfileImg { get; set; }
        public IFormFile Img { get; set; }
    }
}