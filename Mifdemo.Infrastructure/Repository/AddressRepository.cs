using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Models;
using Mifdemo.Infrastructure.Data;

namespace Mifdemo.Infrastructure.Repository
{
    public class AddressRepository : BaseRepository<AddressModel>, IAddressRepository
    {
        private readonly Context _db;
        public AddressRepository(Context context) : base(context)
        {
            _db = context;
        }

        public override async Task<IReadOnlyList<AddressModel>> GetAllAsync()
        {
            return await _db.Addresses
                .Include(c => c.Client)
                .ToListAsync();
        }

        public override async Task<AddressModel> GetByIdAsync(int id)
        {
            return await _db.Addresses
                .Include(c => c.Client)
                .SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}