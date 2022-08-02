using App.Common.Classes;
using App.Common.DTOs.Products;
using App.Domain.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;
		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		[Route("[action]")]
		public ResponseContract<IEnumerable<ProductDTO>> GetAllAsync()
		{
			var response = _productService.GetAllAsync();
			return response;
		}

		[HttpGet]
		[Route("[action]")]
		public ResponseContract<IEnumerable<ProductDTO>> GetAllPagedAsync(DateTime? manufacturingDate, DateTime? validityDate, string providerCode, string providerDescription, string providerPhone, int pageIndex = 0, int pageSize = 0)
		{
			var response = _productService.GetAllPagingAsync(manufacturingDate, validityDate, providerCode, providerDescription, providerPhone, pageIndex, pageSize);
			return response;
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<ResponseContract<ProductDTO>> GetProductByCodeAsync(int code)
		{
			var response = await _productService.FindByIdAsync(code);
			return response;
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<ResponseContract<ProductDTO>> InsertProduct(ProductCreationDTO productDTO)
		{
			var response = await _productService.CreateAsync(productDTO);
			return response;
		}

		[HttpPut]
		[Route("[action]")]
		public async Task<ResponseContract<ProductDTO>> UpdateProduct(ProductDTO productDTO)
		{
			var response = await _productService.UpdateAsync(productDTO);
			return response;
		}

		[HttpDelete]
		[Route("[action]")]
		public async Task<ResponseContract<ProductDTO>> DeleteProduct(int id)
		{
			var response = await _productService.DeleteAsync(id);
			return response;
		}
	}
}
