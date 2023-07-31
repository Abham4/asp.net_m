using System.ComponentModel.DataAnnotations;

namespace mifdemo.ViewModels.Authentication
{
    public class Refresh
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}