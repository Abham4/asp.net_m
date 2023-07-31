using System.Collections.Generic;
using System.Threading.Tasks;
using Mifdemo.Domain.Models;

namespace Mifdemo.Domain.Interface.RepositoryInterface
{
    public interface IDocumentRepository : IBaseRepository<DocumentModel>
    {
        public Task<IReadOnlyList<DocumentModel>> GetDocumentListByReference(int referId);
    }
}