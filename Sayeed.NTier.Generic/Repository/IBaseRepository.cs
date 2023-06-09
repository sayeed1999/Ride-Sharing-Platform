﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sayeed.NTier.Generic.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        #region basic crud
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> FindByIdAsync(long id); // FindAsync() is only for PK's!
        public Task AddAsync(T item);
        public void Update(T item);
        public void UpdateById(long id, T item);
        public void Delete(T item);
        public Task DeleteByIdAsync(long id);
        #endregion

        public Task<int> SaveChangesAsync();

        #region advanced crud
        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        public IQueryable<T> Where(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        public Task<long> CountAsync();
        public Task<long> CountAsync(Expression<Func<T, bool>> filter);
        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        public Task<T> LastOrDefaultAsync(params Expression<Func<T, object>>[] includes);
        public Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        public Task<IEnumerable<T>> ToListAsync();
        public IQueryable<T> GetByWhereClause(Expression<Func<T, bool>> filter, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes);
        public Task<T> GetBySingleOrDefaultAsync(Expression<Func<T, bool>> filter, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes);
        public Task<T> GetByFirstOrDefaultAsync(Expression<Func<T, bool>> filter, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes);
        public IQueryable<T> FromSql(string rawsql, params SqlParameter[] parameters);
        #endregion

    }
}
