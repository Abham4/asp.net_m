using System;
using System.Data;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mifdemo.Domain.Interface.RepositoryInterface
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsInTransaction { get; }
        Task SaveChanges();
        Task SaveChanges(SaveOptions saveOptions);
        Task BeginTransaction();
        Task BeginTransaction(IsolationLevel isolationLevel);
        Task RollBackTransaction();
        Task CommitTransaction();
    }
}