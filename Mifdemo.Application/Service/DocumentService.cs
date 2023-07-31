using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Interface.ServiceInterface;
using Mifdemo.Domain.Models;

namespace Mifdemo.Application.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _docs;

        public DocumentService(IDocumentRepository repository)
        {
            _docs = repository;
        }

        public async Task DeleteDocumentAsync(int id)
        {
            var docs = await _docs.GetByIdAsync(id);
            await _docs.DeleteAsync(docs);
            await _docs.UnitOfWork.SaveChanges();
        }

        public async Task<List<DocumentModel>> GetallDocumentAsync()
        {
            var all = await _docs.GetAllAsync();
            return all.ToList();
        }

        public async Task<DocumentModel> GetDocument(int id)
        {
            return await _docs.GetByIdAsync(id);
        }

        public async Task<List<DocumentModel>> GetDocumentsListByRefence(int referId)
        {
            var all = await _docs.GetDocumentListByReference(referId);
            return all.ToList();
        }

        public async Task PostDocumentAsync(DocumentModel document)
        {
            await _docs.AddAsync(document);
            await _docs.UnitOfWork.SaveChanges();
        }

        public async Task UpdateDocumentAsync(DocumentModel document)
        {
            await _docs.UpdateAsync(document);
            await _docs.UnitOfWork.SaveChanges();
        }
    }
}