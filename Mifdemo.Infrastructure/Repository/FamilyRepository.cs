using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Models;
using Mifdemo.Infrastructure.Data;

namespace Mifdemo.Infrastructure.Repository
{
    public class FamilyRepository : BaseRepository<FamilyMembersModel>, IFamilyRepository
    {
        private readonly Context _db;
        public FamilyRepository(Context context) : base(context)
        {
            _db = context;
        }

        public override async Task<IReadOnlyList<FamilyMembersModel>> GetAllAsync()
        {
            return await _db.Families
                .Include(c => c.Client)
                .ToListAsync();
        }

        public override async Task<FamilyMembersModel> GetByIdAsync(int id)
        {
            return await _db.Families
                .Include(c => c.Client)
                .SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}