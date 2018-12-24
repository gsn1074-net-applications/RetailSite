using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSite.Products.Api.Mapping.AutoMapperProfiles
{
	public class ProductsProfile : Profile
	{
		public ProductsProfile()
		{
			CreateMap<DAL.Entities.Product, DTO.Read.Product>()
				.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

			CreateMap<DTO.Create.Product, DAL.Entities.Product>();

			CreateMap<DAL.Entities.Product, DTO.Read.ProductWithImages>()
				.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

			CreateMap<IEnumerable<DTO.Backend.ProductImage>, DTO.Read.ProductWithImages>()
				.ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src));
		}
	}
}
