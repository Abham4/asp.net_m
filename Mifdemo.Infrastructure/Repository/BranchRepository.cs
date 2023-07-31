using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Models;
using Mifdemo.Infrastructure.Data;

namespace Mifdemo.Infrastructure.Repository
{
    public class BranchRepository : BaseRepository<BranchModel>, IBranchRepository
    {
        private readonly Context _con;
        public BranchRepository(Context context): base(context)
        {
            _con = context;
        }

        public override async Task<IReadOnlyList<BranchModel>> GetAllAsync()
        {
            return await _con.Branches
                .Include(c => c.Users)
                .Include(c => c.Vouchers)
                .Include(c => c.Clients)
                .AsSingleQuery()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<BranchModel>> GetBranchesByAddress(string address)
        {
            return await _con.Branches.Where(c => c.Address == address)
                .Include(c => c.Users)
                .Include(c => c.Vouchers)
                .Include(c => c.Clients)
                .AsSingleQuery()
                .ToListAsync();
        }

        public async Task<BranchModel> GetBranchesByName(string name)
        {
            return await _con.Branches
                .Include(c => c.Users)
                .Include(c => c.Vouchers)
                .Include(c => c.Clients)
                .AsSingleQuery()
                .SingleOrDefaultAsync(c => c.Name == name);
        }

        public override async Task<BranchModel> GetByIdAsync(int id)
        {
            return await _con.Branches
                .Include(c => c.Users)
                .Include(c => c.Vouchers)
                .Include(c => c.Clients)
                .AsSingleQuery()
                .SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}