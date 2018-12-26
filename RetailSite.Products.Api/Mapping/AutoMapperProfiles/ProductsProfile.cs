using AutoMapper;
using System.Collections.Generic;

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

			CreateMap<DAL.Entities.Product, DTO.Update.Product>();

			CreateMap<DTO.Update.Product, DAL.Entities.Product>();
		}
	}
}
