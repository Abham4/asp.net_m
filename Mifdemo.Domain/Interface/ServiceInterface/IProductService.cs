using Mifdemo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifdemo.Domain.Interface.ServiceInterface
{
    public interface IProductService
    {
        public Task<List<ProductModel>> GetallProductsAsync();
        public Task<ProductModel> GetProductsAsync(int id);
        public Task PostProductsAsync(ProductModel product);
        public Task UpdateProductsAsync(ProductModel product);
        public Task DeleteProductsAsync(int id);
    }
}
