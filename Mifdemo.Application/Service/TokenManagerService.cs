using System.Threading.Tasks;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Interface.ServiceInterface;
using Mifdemo.Domain.Models;

namespace Mifdemo.Application.Service
{
    public class TokenManagerService : ITokenManagerService
    {
        private readonly ITokenManagerRepository _repo;
        public TokenManagerService(ITokenManagerRepository repository)
        {
            _repo = repository;
        }
        public async Task<TokenManagerModel> GetUserId(string refreshToken)
        {
            return await _repo.GetUserId(refreshToken);
        }

        public async Task PostTokenAsync(TokenManagerModel tokenManager)
        {
            await _repo.AddAsync(tokenManager);
            await _repo.UnitOfWork.SaveChanges();
        }

        public async Task PutTokenAsync(TokenManagerModel tokenManager)
        {
            await _repo.UpdateAsync(tokenManager);
            await _repo.UnitOfWork.SaveChanges();
        }
    }
}