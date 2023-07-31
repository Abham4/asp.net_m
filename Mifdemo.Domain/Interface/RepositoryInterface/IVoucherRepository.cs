using System.Collections.Generic;
using System.Threading.Tasks;
using Mifdemo.Domain.Models;

namespace Mifdemo.Domain.Interface.RepositoryInterface
{
    public interface IVoucherRepository : IBaseRepository<VoucherModel>
    {
        public Task<IReadOnlyList<VoucherModel>> GetVouchersListByClient(int clientId);
    }
}