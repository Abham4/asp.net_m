using System.Collections.Generic;
using System.Threading.Tasks;
using Mifdemo.Domain.Models;

namespace Mifdemo.Domain.Interface.ServiceInterface
{
    public interface IVoucherService
    {
        public Task<List<VoucherModel>> GetVouchers();
        public Task<VoucherModel> GetVoucherAsync(int purchasedProductId);
        public Task PostVoucherAsync(VoucherModel voucher);
        public Task<List<VoucherModel>> GetVouchersListByClient(int clientId);
    }
}