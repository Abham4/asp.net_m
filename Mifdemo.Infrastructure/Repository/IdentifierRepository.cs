using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Models;
using Mifdemo.Infrastructure.Data;

namespace Mifdemo.Infrastructure.Repository
{
    public class IdentifierRepository : BaseRepository<IdentifierModel>, IIdentifierRepository
    {
        private readonly Context _db;
        public IdentifierRepository(Context context) : base(context)
        {
            _db = context;
        }

        public override async Task<IReadOnlyList<IdentifierModel>> GetAllAsync()
        {
            return await _db.Identifiers
                .Include(c => c.Client)
                .ToListAsync();
        }

        public override async Task<IdentifierModel> GetByIdAsync(int id)
        {
            return await _db.Identifiers
                .Include(c => c.Client)
                .SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}