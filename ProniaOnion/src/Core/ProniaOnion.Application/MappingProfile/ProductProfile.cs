using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Colors;
using ProniaOnion.Application.DTOs.Products;
using ProniaOnion.Application.DTOs.Sizes;
using ProniaOnion.Application.DTOs.Tags;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfile
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductItemDTO>().ReverseMap();
            //CreateMap<Product, GetProductDTO>()
            //    .ConvertUsing(p=>new GetProductDTO(
            //        p.Id, 
            //        p.Name, 
            //        p.SKU, 
            //        p.Description,
            //        new CategoryItemDTO(p.CategoryId, p.Category.Name),
            //        p.ProductColors.Select(pc=> new ColorItemDTO(pc.ColorId, pc.Color.Name)
            //        )));



            CreateMap<Product, GetProductDTO>()
                .ForCtorParam(
                nameof(GetProductDTO.Colors),
                opt => opt.MapFrom(
                    p => p.ProductColors.Select(pc => new ColorItemDTO(pc.ColorId, pc.Color.Name)).ToList() )
                    )
                 .ForCtorParam(
                nameof(GetProductDTO.Sizes),
                opt => opt.MapFrom(
                    p => p.ProductSize.Select(ps => new SizeItemDTO(ps.SizeId, ps.Size.Name)).ToList()
                    ))
                 .ForCtorParam(
                nameof(GetProductDTO.Tags),
                opt => opt.MapFrom(
                    p => p.ProductTags.Select(ps => new TagItemDTO(ps.TagId, ps.Tag.Name)).ToList()    
                    ));


            CreateMap<CreateProductDTO, Product>().ForMember(
                p => p.ProductColors,
                opt => opt.MapFrom(pDTO => pDTO.ColorsIds.Select(ci => new ProductColor { ColorId = ci })));


            CreateMap<UpdateProductDTO, Product>()
                .ForMember(
                p=>p.Id,
                opt=>opt.Ignore())
                .ForMember(
                p => p.ProductColors,
                opt => opt.MapFrom(pDTO => pDTO.ColorsIds.Select(ci => new ProductColor { ColorId = ci })));


        }
    }
}
