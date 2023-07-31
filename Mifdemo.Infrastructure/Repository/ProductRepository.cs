using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Models;
using Mifdemo.Infrastructure.Data;

namespace Mifdemo.Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<ProductModel>, IProductRepository
    {
        private readonly Context _db;
        public ProductRepository(Context context) : base(context)
        {
            _db = context;
        }

        public override async Task<IReadOnlyList<ProductModel>> GetAllAsync()
        {
            return await _db.Products
                .Include(c => c.PurchasedProducts)
                .ToListAsync();
        }

        public override async Task<ProductModel> GetByIdAsync(int id)
        {
            return await _db.Products
                .Include(c => c.PurchasedProducts)
                .SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}