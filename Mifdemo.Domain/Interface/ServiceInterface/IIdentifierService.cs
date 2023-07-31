using System.Threading.Tasks;
using Mifdemo.Domain.Models;

namespace Mifdemo.Domain.Interface.ServiceInterface
{
    public interface IIdentifierService
    {
        public Task PostIdentifierAsync(IdentifierModel identifier);
        public Task UpdateIdentifierAsync(IdentifierModel identifier);
        public Task DeleteIdentifierAsync(int Id);
    }
}