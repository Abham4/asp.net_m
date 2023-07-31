using System.Threading.Tasks;
using Mifdemo.Domain.Models;

namespace Mifdemo.Domain.Interface.ServiceInterface
{
    public interface ITokenManagerService
    {
        public Task PostTokenAsync(TokenManagerModel tokenManager);
        public Task PutTokenAsync(TokenManagerModel tokenManager);
        public Task<TokenManagerModel> GetUserId(string refreshToken);

    }
}