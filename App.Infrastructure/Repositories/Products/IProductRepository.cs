using App.Infrastructure.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories.Products
{
	public interface IProductRepository
	{
		Task<ProductEntity> CreateAsync(ProductEntity entity);
		Task DeleteAsync(ProductEntity entity);
		Task<ProductEntity> FindByIdAsync(object id);
		IQueryable<ProductEntity> GetAll();
		IQueryable<ProductEntity> GetFilteredProductsPaged(DateTime? manufacturingDate, DateTime? validityDate, string providerCode, string providerDescription, string providerPhone, int pageIndex, int pageSize);
		Task SaveChangesAsync();
		Task<ProductEntity> UpdateAsync(ProductEntity entity);
	}
}
