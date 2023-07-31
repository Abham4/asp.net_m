using System.Collections.Generic;
using System.Threading.Tasks;
using Mifdemo.Domain.Models;

namespace Mifdemo.Domain.Interface.ServiceInterface
{
    public interface IDocumentService
    {
        public Task PostDocumentAsync(DocumentModel document);
        public Task UpdateDocumentAsync(DocumentModel document);
        public Task DeleteDocumentAsync(int id);
        public Task<List<DocumentModel>> GetDocumentsListByRefence(int referId);
        public Task<DocumentModel> GetDocument(int id);
        public Task<List<DocumentModel>> GetallDocumentAsync();
    }
}