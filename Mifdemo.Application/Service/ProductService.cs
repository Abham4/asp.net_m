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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _bs;
        public ProductService(IProductRepository Repository)
        {
            _bs = Repository;
        }
        public async Task<List<ProductModel>> GetallProductsAsync()
        {
            var all = await _bs.GetAllAsync();
            return all.ToList();
        }
        public async Task<ProductModel> GetProductsAsync(int id)
        {
            return await _bs.GetByIdAsync(id);
        }
        public async Task PostProductsAsync(ProductModel product)
        {
            await _bs.AddAsync(product);
            await _bs.UnitOfWork.SaveChanges();
        }
        public async Task UpdateProductsAsync(ProductModel product)
        {
            await _bs.UpdateAsync(product);
            await _bs.UnitOfWork.SaveChanges();
        }
        public async Task DeleteProductsAsync(int id)
        {
            var client = await _bs.GetByIdAsync(id);
            await _bs.DeleteAsync(client);
            await _bs.UnitOfWork.SaveChanges();
        }
    }
}
