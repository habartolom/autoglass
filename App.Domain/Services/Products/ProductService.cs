using App.Common.Classes;
using App.Common.DTOs.Products;
using App.Common.Enums;
using App.Infrastructure.Database.Entities;
using App.Infrastructure.Repositories;
using App.Infrastructure.Repositories.Products;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Products
{
	public class ProductService : IProductService
	{
		public IProductRepository _repository;
		public IMapper _mapperDependency;

		public ProductService(IProductRepository repository, IMapper mapperDependency)
		{
			_repository = repository;
			_mapperDependency = mapperDependency;
		}

		public async Task<ResponseContract<ProductDTO>> CreateAsync(ProductCreationDTO dto)
		{
			ResponseContract<ProductDTO> response = new ResponseContract<ProductDTO>();

			if (dto == null)
			{
				response.Header.Code = HttpCodes.BadRequest;
				response.Header.Message = "El objeto no puede ser nulo";
				return response;
			}

			if (dto.ManufacturingDate >= dto.ValidityDate)
			{
				response.Header.Code = HttpCodes.BadRequest;
				response.Header.Message = "La fecha de fabricación no puede ser mayor o igual a la fecha de vencimiento";
				return response;
			}

			try
			{
				ProductEntity entity = _mapperDependency.Map<ProductEntity>(dto);
				entity = await _repository.CreateAsync(entity);
				response.Data = _mapperDependency.Map<ProductDTO>(entity);
			}
			catch (Exception ex)
			{
				response.Header.Code = HttpCodes.InternalServerError;
				response.Header.Message = ex.Message;
			}
			return response;
		}

		public async Task<ResponseContract<ProductDTO>> DeleteAsync(int id)
		{
			ResponseContract<ProductDTO> response = new ResponseContract<ProductDTO>();

			try
			{
				ProductEntity entity = await _repository.FindByIdAsync(id);
				if (entity == null)
					throw new ApplicationException("Elemento no encontrado");

				entity.Status = false;
				ProductEntity result = await _repository.UpdateAsync(entity);
				response.Data = _mapperDependency.Map<ProductDTO>(result);
			}
			catch (Exception ex)
			{
				response.Header.Code = HttpCodes.InternalServerError;
				response.Header.Message = ex.Message;
			}
			return response;
		}
		
		public async Task<ResponseContract<ProductDTO>> FindByIdAsync(int Id)
		{
			ResponseContract<ProductDTO> response = new ResponseContract<ProductDTO>();

			try
			{
				ProductEntity result = await _repository.FindByIdAsync(Id);
				response.Data =  _mapperDependency.Map<ProductDTO>(result);
			}
			catch (Exception ex)
			{
				response.Header.Code = HttpCodes.InternalServerError;
				response.Header.Message = ex.Message;
			}
			return response;
		}

		public ResponseContract<IEnumerable<ProductDTO>> GetAllAsync()
		{
			ResponseContract<IEnumerable<ProductDTO>> response = new ResponseContract<IEnumerable<ProductDTO>>();

			try
			{
				response.Data = _mapperDependency.Map<List<ProductDTO>>(_repository.GetAll());
			}
			catch (Exception ex)
			{
				response.Header.Code = HttpCodes.InternalServerError;
				response.Header.Message = ex.Message;
			}
			return response;
		}

		public ResponseContract<IEnumerable<ProductDTO>> GetAllPagingAsync(DateTime? manufacturingDate, DateTime? validityDate, string providerCode, string providerDescription, string providerPhone, int pageIndex, int pageSize)
		{
			ResponseContract<IEnumerable<ProductDTO>> response = new ResponseContract<IEnumerable<ProductDTO>>();
			
			if(pageIndex < 0 || pageSize < 0){
				response.Header.Code = HttpCodes.BadRequest;
				response.Header.Message = "Los valores para pageIndex y pageSize deben ser mayores a 0";
				return response;
			}

			try
			{
				response.Data = _mapperDependency.Map<List<ProductDTO>>(_repository.GetFilteredProductsPaged(manufacturingDate, validityDate, providerCode, providerDescription, providerPhone, pageIndex, pageSize));
			}
			catch (Exception ex)
			{
				response.Header.Code = HttpCodes.InternalServerError;
				response.Header.Message = ex.Message;
			}
			return response;
		}

		public async Task<ResponseContract<ProductDTO>> UpdateAsync(ProductDTO dto)
		{
			ResponseContract<ProductDTO> response = new ResponseContract<ProductDTO>();

			if (dto == null)
			{
				response.Header.Code = HttpCodes.BadRequest;
				response.Header.Message = "El objeto no puede ser nulo";
				return response;
			}

			if (dto.ManufacturingDate >= dto.ValidityDate)
			{
				response.Header.Code = HttpCodes.BadRequest;
				response.Header.Message = "La fecha de fabricación no puede ser mayor o igual a la fecha de vencimiento";
				return response;
			}

			try
			{
				ProductEntity entity = _mapperDependency.Map<ProductEntity>(dto);
				entity = await _repository.UpdateAsync(entity);
				response.Data = _mapperDependency.Map<ProductDTO>(entity);
			}
			catch (Exception ex)
			{
				response.Header.Code = HttpCodes.InternalServerError;
				response.Header.Message = ex.Message;
			}
			return response;
		}
	}
}
