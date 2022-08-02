using App.Common.DTOs.Products;
using App.Infrastructure.Database.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Config.Dependencies
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<ProductEntity, ProductDTO>()
				.ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status))
				.ReverseMap();

			CreateMap<ProductEntity, ProductCreationDTO>()
				.ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status))
				.ReverseMap();

		}
	}
}
