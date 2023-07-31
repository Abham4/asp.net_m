using System;
using Mifdemo.Domain.Seed;

namespace Mifdemo.Domain.Models
{
    public class TokenManagerModel : BaseAuditModel
    {
        public string UserId { get; set; }
        public ApplicationUserModel User { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool Used { get; set; }
    }
}