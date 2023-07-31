using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Interface.ServiceInterface;
using Mifdemo.Domain.Models;

namespace Mifdemo.Application.Service
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _repo;
        public VoucherService(IVoucherRepository repository)
        {
            _repo = repository;
        }
        public async Task<VoucherModel> GetVoucherAsync(int purchasedProductId)
        {
            return await _repo.GetByIdAsync(purchasedProductId);
        }

        public async Task<List<VoucherModel>> GetVouchers()
        {
            var vouchers = await _repo.GetAllAsync();
            return vouchers.ToList();
        }

        public async Task<List<VoucherModel>> GetVouchersListByClient(int clientId)
        {
            var all = await _repo.GetVouchersListByClient(clientId);
            return all.ToList();
        }

        public async Task PostVoucherAsync(VoucherModel voucher)
        {
            await _repo.AddAsync(voucher);
        }
    }
}