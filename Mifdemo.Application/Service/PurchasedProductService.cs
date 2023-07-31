using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Interface.ServiceInterface;
using Mifdemo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifdemo.Application.Service
{
    public class PurchasedProductService : IPurchasedProductService
    {
        private readonly IPurchasedProductRepository _bs;
        public PurchasedProductService(IPurchasedProductRepository aplRepository)
        {
            _bs = aplRepository;
        }
        public async Task<List<PurchasedProductModel>> GetallPurchasedProductAsync()
        {
            var all = await _bs.GetAllAsync();
            return all.ToList();
        }
        public async Task<PurchasedProductModel> GetPurchasedProductAsync(int id)
        {
            return await _bs.GetByIdAsync(id);
        }
        public async Task PostPurchasedProductAsync(PurchasedProductModel purchasedProduct)
        {
            await _bs.AddAsync(purchasedProduct);
        }
        public async Task UpdatePurchasedProductAsync(PurchasedProductModel purchasedProduct)
        {
            await _bs.UpdateAsync(purchasedProduct);
            await _bs.UnitOfWork.SaveChanges();
        }
        public async Task DeletePurchasedProductAsync(int id)
        {
            var purchasedProduct = await _bs.GetByIdAsync(id);
            await _bs.DeleteAsync(purchasedProduct);
            await _bs.UnitOfWork.SaveChanges();
        }
    }
}
