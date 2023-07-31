using Microsoft.AspNetCore.Http;

namespace mifdemo.ViewModels.Accounts
{
    public class AccountsViewModel
    {
        
        public string AccountType { get; set; }
        public int GlCode { get; set; }
        public string AccountUsage { get; set; }
        public string Parent { get; set; }
        public string AccountName { get; set; }
        public string Tag { get; set; }
        public bool ManualEntriesAllowed { get; set; }
        public string Description { get; set; }
        public IFormFile Signature { get; set; }
    }
}