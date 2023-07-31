using System.ComponentModel.DataAnnotations;

namespace mifdemo.ViewModels.Authentication
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}