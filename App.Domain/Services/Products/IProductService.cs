using App.Common.Classes;
using App.Common.DTOs.Products;
using App.Infrastructure.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Products
{
	public interface IProductService
	{
		Task<ResponseContract<ProductDTO>> CreateAsync(ProductCreationDTO dto);
		Task<ResponseContract<ProductDTO>> DeleteAsync(int id);
		Task<ResponseContract<ProductDTO>> FindByIdAsync(int Id);
		ResponseContract<IEnumerable<ProductDTO>> GetAllAsync();
		ResponseContract<IEnumerable<ProductDTO>> GetAllPagingAsync(DateTime? manufacturingDate, DateTime? validityDate, string providerCode, string providerDescription, string providerPhone, int pageIndex, int pageSize);
		Task<ResponseContract<ProductDTO>> UpdateAsync(ProductDTO dto);
	}
}
