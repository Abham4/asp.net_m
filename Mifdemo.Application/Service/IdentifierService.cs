using System.Threading.Tasks;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Interface.ServiceInterface;
using Mifdemo.Domain.Models;

namespace Mifdemo.Application.Service
{
    public class IdentifierService : IIdentifierService
    {
        private readonly IIdentifierRepository _identifier;

        public IdentifierService(IIdentifierRepository repository)
        {
            _identifier = repository;
        }

        public async Task DeleteIdentifierAsync(int id)
        {
            var identifier = await _identifier.GetByIdAsync(id);
            await _identifier.DeleteAsync(identifier);
            await _identifier.UnitOfWork.SaveChanges();
        }

        public async Task PostIdentifierAsync(IdentifierModel identifier)
        {
            await _identifier.AddAsync(identifier);
            await _identifier.UnitOfWork.SaveChanges();
        }

        public async Task UpdateIdentifierAsync(IdentifierModel identifier)
        {
            await _identifier.UpdateAsync(identifier);
            await _identifier.UnitOfWork.SaveChanges();
        }
    }
}