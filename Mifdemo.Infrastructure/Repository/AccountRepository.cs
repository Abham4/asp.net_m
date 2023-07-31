using Microsoft.EntityFrameworkCore;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Models;
using Mifdemo.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifdemo.Infrastructure.Repository
{
    public class AccountRepository : BaseRepository<AccountModel>, IAccountRepository
    {
        private readonly Context _db;
        public AccountRepository(Context context):base(context)
        {
            _db = context;
        }
        
        public override async Task<IReadOnlyList<AccountModel>> GetAllAsync()
        {
            return await _db.Account
                .Include(c => c.Clients)
                .Include(c => c.PurchasedProducts)
                .AsSingleQuery()
                .ToListAsync();
        }

        public override async Task<AccountModel> GetByIdAsync(int id)
        {
            return await _db.Account
                .Include(c => c.Clients)
                .Include(c => c.PurchasedProducts)
                .AsSingleQuery()
                .SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}
