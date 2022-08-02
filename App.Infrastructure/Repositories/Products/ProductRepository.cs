using App.Infrastructure.Database.Context;
using App.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories.Products
{
	public class ProductRepository : IProductRepository
	{
		protected DbContext _Database;
		protected DbSet<ProductEntity> _Table;

		public ProductRepository(AutoglassContext dbContext)
		{
			_Database = dbContext;
			_Table = _Database.Set<ProductEntity>();
		}

		public async Task<ProductEntity> CreateAsync(ProductEntity entity)
		{
			await _Table.AddAsync(entity);
			await SaveChangesAsync();

			return entity;
		}

		public async Task DeleteAsync(ProductEntity entity)
		{
			_Table.Remove(entity);
			await SaveChangesAsync();
		}

		public async Task<ProductEntity> FindByIdAsync(object id)
		{
			return await _Table.FindAsync(id);
		}

		public IQueryable<ProductEntity> GetAll()
		{
			return _Table;
		}

		public IQueryable<ProductEntity> GetFilteredProductsPaged(DateTime? manufacturingDate, DateTime? validityDate, string providerCode, string providerDescription, string providerPhone, int pageIndex, int pageSize)
		{
			var response = _Table.AsQueryable();

			if (manufacturingDate != null)
				response = response.Where(x => x.ManufacturingDate.Equals(manufacturingDate));

			if (validityDate != null)
				response = response.Where(x => x.ValidityDate.Equals(validityDate));

			if (!string.IsNullOrWhiteSpace(providerCode))
				response = response.Where(x => x.ProviderCode.Equals(providerCode));

			if (!string.IsNullOrWhiteSpace(providerDescription))
				response = response.Where(x => x.ProviderDescription.Equals(providerDescription));

			if (!string.IsNullOrWhiteSpace(providerPhone))
				response = response.Where(x => x.ProviderPhone.Equals(providerPhone));
			
			if (pageIndex > 0 && pageSize > 0)
				response = response.Skip((pageIndex - 1) * pageSize).Take(pageSize);

			return response;
		}

		public IQueryable<ProductEntity> GetAllPaged(int pageIndex, int pageSize)
		{
			return _Table.Skip((pageIndex - 1) * pageSize).Take(pageSize);
		}

		public async Task SaveChangesAsync()
		{
			await _Database.SaveChangesAsync();
		}

		public async Task<ProductEntity> UpdateAsync(ProductEntity entity)
		{
			ProductEntity oldItem = await FindByIdAsync(entity.Code);
			_Database.Entry(oldItem).CurrentValues.SetValues(entity);
			await SaveChangesAsync();

			return entity;
		}
	}
}
