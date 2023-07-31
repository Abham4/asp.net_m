using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mifdemo.Domain.Seed;

namespace Mifdemo.Domain.Interface.RepositoryInterface
{
    public interface IBaseRepository <T> where T : BaseAuditModel
    {
        IUnitOfWork UnitOfWork { get; }
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetQueryAsync(Expression<Func<T, bool>> predicate);
    }
}