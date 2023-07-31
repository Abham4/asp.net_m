using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Models;
using Mifdemo.Infrastructure.Data;

namespace Mifdemo.Infrastructure.Repository
{
    public class DocumentRepository : BaseRepository<DocumentModel>, IDocumentRepository
    {
        private readonly Context _db;
        public DocumentRepository(Context context) : base(context)
        {
            _db = context;
        }

        public async Task<IReadOnlyList<DocumentModel>> GetDocumentListByReference(int referId)
        {
            return await _db.Documents.Where(c => c.Reference == referId).ToListAsync();
        }
    }
}