using Mifdemo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifdemo.Domain.Interface.ServiceInterface
{
    public interface IPurchasedProductService
    {
        public Task<List<PurchasedProductModel>> GetallPurchasedProductAsync();
        public Task<PurchasedProductModel> GetPurchasedProductAsync(int Id);
        public Task PostPurchasedProductAsync(PurchasedProductModel purchasedProduct);
        public Task UpdatePurchasedProductAsync(PurchasedProductModel purchasedProduct);
        public Task DeletePurchasedProductAsync(int Id);
    }
}
