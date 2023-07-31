using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Domain.Seed;
using Mifdemo.Infrastructure.Data;

namespace Mifdemo.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseAuditModel
    {
        private readonly DbContext _context;
        private IUnitOfWork unitOfWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                if(unitOfWork == null)
                {
                    unitOfWork = new UnitOfWork(this._context);
                }
                return unitOfWork;
            }
            set
            {
                unitOfWork = new UnitOfWork(this._context);
            }
        }

        public BaseRepository(Context con)
        {
            _context = con ?? throw new ArgumentNullException("context");
        }

        public virtual async Task AddAsync(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException("entity");
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual async Task DeleteAsync(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException("entity");
            _context.Set<T>().Remove(entity);
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetQueryAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException("entity");
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }
    }
}