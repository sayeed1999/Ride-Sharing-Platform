﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure.Repositories
{
	// TODO: use primary key type generically
	public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
	{

		#region initializations & declarations

		protected readonly ApplicationDbContext _dbContext;
		protected readonly DapperContext _dapperContext;
		protected DbSet<T> _dbSet;

		public BaseRepository(ApplicationDbContext dbContext, DapperContext dapperContext)
		{
			_dbContext = dbContext;
			_dapperContext = dapperContext;
			_dbSet = dbContext.Set<T>();
		}

		#endregion

		#region Transactional helpers

		public virtual async Task<IDbContextTransaction> BeginTransactionAsync()
		{
			var transaction = await _dbContext.Database.BeginTransactionAsync();
			return transaction;
		}

		public virtual async Task CommitTransactionAsync(IDbContextTransaction transaction)
		{
			await transaction.CommitAsync();
		}

		public virtual async Task RollBackTransactionAsync(IDbContextTransaction transaction)
		{
			await transaction.RollbackAsync();
		}

		#endregion

		public virtual async Task<T> FindByIdAsync(long id)
		{
			return await _dbSet.FindAsync(id);
		}

		public virtual async Task CreateAsync(T item)
		{
			item.ResetPrimaryKey();
			item.Created();

			await _dbSet.AddAsync(item);
		}

		public virtual async Task BulkCreateAsync(ICollection<T> items)
		{
			foreach (var item in items)
			{
				item.ResetPrimaryKey();
				item.Created();
			}

			await _dbSet.AddRangeAsync(items);
		}

		public virtual void Update(T item)
		{
			item.Modified();

			_dbSet.Update(item);
		}

		public virtual void BulkUpdate(ICollection<T> items)
		{
			foreach (var item in items)
			{
				item.Modified();
			}

			_dbSet.UpdateRange(items);
		}

		public virtual void Delete(T item)
		{
			_dbSet.Remove(item);
		}

		public virtual void BulkDelete(ICollection<T> items)
		{
			foreach (var item in items)
			{
				item.Deleted();
			}

			_dbSet.RemoveRange(items);
		}

		public virtual async Task<int> SaveChangesAsync()
		{
			return await _dbContext.SaveChangesAsync();
		}
	}
}
