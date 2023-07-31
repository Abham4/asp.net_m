using System;
using System.ComponentModel.DataAnnotations;
using Mifdemo.Domain.Seed;

namespace Mifdemo.Domain.Models
{
    public class FamilyMembersModel : BaseAuditModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Qualification { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public bool isDepandant { get; set; }
        public string RelationShip { get; set; }
        public string Gender { get; set; }
        public string Profession { get; set; }

        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime DOB { get; set; }
        public int ClientId { get; set; }
        public ClientModel Client { get; set; }
    }
}