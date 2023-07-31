
using Microsoft.EntityFrameworkCore;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Models;
using Mifdemo.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mifdemo.Infrastructure.Repository
{
    public class AccountsRepository : BaseRepository<AccountsModel>, IAccountsRepository
    {
        private readonly Context _db;
        public AccountsRepository(Context context) : base(context)
        {
            _db = context;
        }

        public override async Task<IReadOnlyList<AccountsModel>> GetAllAsync()
        {
            return await _db.Accounts.ToListAsync();
        }
    }
}