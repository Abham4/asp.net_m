using Mifdemo.Domain.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifdemo.Domain.Models
{
    public class AccountsModel : BaseAuditModel
    {
        public string AccountType { get; set; }
        public int GlCode { get; set; }
        public string AccountUsage { get; set; }
        public string Parent { get; set; }
        public string AccountName { get; set; }
        public string Tag { get; set; }
        public bool ManualEntriesAllowed { get; set; }
        public string Description { get; set; }
        public byte[] Signature { get; set; }
        
    }
}