using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Mifdemo.Domain.Models
{
    public class ApplicationUserModel : IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        public int BranchId { get; set; }
        public BranchModel Branch { get; set; }
        public string ProfileImg { get; set; }
    }
}