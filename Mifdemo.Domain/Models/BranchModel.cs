using System.Collections.Generic;
using Mifdemo.Domain.Seed;

namespace Mifdemo.Domain.Models
{
    public class BranchModel : BaseAuditModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<ApplicationUserModel> Users { get; set; }
        public List<VoucherModel> Vouchers { get; set; }
        public List<ClientModel> Clients { get; set; }
    }
}