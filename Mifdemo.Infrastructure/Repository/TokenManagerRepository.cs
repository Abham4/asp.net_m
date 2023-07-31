using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Models;
using Mifdemo.Infrastructure.Data;

namespace Mifdemo.Infrastructure.Repository
{
    public class TokenManagerRepository : BaseRepository<TokenManagerModel>, ITokenManagerRepository
    {
        private readonly Context _con;
        public TokenManagerRepository(Context context) : base(context)
        {
            _con = context;
        }

        public async Task<TokenManagerModel> GetUserId(string refreshToken)
        {
            return await _con.TokenManagers.SingleOrDefaultAsync(c => c.RefreshToken == refreshToken);
        }
    }
}