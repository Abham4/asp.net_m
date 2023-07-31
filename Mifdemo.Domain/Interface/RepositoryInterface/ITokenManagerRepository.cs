using System.Collections.Generic;
using System.Threading.Tasks;
using Mifdemo.Domain.Models;

namespace Mifdemo.Domain.Interface.RepositoryInterface
{
    public interface ITokenManagerRepository : IBaseRepository<TokenManagerModel>
    {
        public Task<TokenManagerModel> GetUserId(string refreshToken);
    }
}