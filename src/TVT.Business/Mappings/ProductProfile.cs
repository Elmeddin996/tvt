using AutoMapper;
using TVT.Business.DTOs.Products;
using TVT.Core.Entities;

namespace TVT.Business.Mappings;

public sealed class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductListDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameAz))
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.SlugAz))
            .ForMember(dest => dest.Image, opt => opt.Ignore());

        CreateMap<Product, ProductDetailDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameAz))
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.SlugAz))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.DescriptionAz))
            .ForMember(dest => dest.Image, opt => opt.Ignore());

        CreateMap<CreateProductDto, Product>()
            .ForMember(dest => dest.NameAz, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.NameRu, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.SlugAz, opt => opt.MapFrom(src => src.Slug))
            .ForMember(dest => dest.SlugEn, opt => opt.MapFrom(src => src.Slug))
            .ForMember(dest => dest.SlugRu, opt => opt.MapFrom(src => src.Slug))
            .ForMember(dest => dest.DescriptionAz, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DescriptionEn, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DescriptionRu, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Brand, opt => opt.Ignore())
            .ForMember(dest => dest.SeoTitleAz, opt => opt.Ignore())
            .ForMember(dest => dest.SeoTitleEn, opt => opt.Ignore())
            .ForMember(dest => dest.SeoTitleRu, opt => opt.Ignore())
            .ForMember(dest => dest.SeoDescriptionAz, opt => opt.Ignore())
            .ForMember(dest => dest.SeoDescriptionEn, opt => opt.Ignore())
            .ForMember(dest => dest.SeoDescriptionRu, opt => opt.Ignore())
            .ForMember(dest => dest.SeoKeywordsAz, opt => opt.Ignore())
            .ForMember(dest => dest.SeoKeywordsEn, opt => opt.Ignore())
            .ForMember(dest => dest.SeoKeywordsRu, opt => opt.Ignore());

        CreateMap<UpdateProductDto, Product>()
            .ForMember(dest => dest.NameAz, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.NameRu, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.SlugAz, opt => opt.MapFrom(src => src.Slug))
            .ForMember(dest => dest.SlugEn, opt => opt.MapFrom(src => src.Slug))
            .ForMember(dest => dest.SlugRu, opt => opt.MapFrom(src => src.Slug))
            .ForMember(dest => dest.DescriptionAz, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DescriptionEn, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DescriptionRu, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Brand, opt => opt.Ignore())
            .ForMember(dest => dest.SeoTitleAz, opt => opt.Ignore())
            .ForMember(dest => dest.SeoTitleEn, opt => opt.Ignore())
            .ForMember(dest => dest.SeoTitleRu, opt => opt.Ignore())
            .ForMember(dest => dest.SeoDescriptionAz, opt => opt.Ignore())
            .ForMember(dest => dest.SeoDescriptionEn, opt => opt.Ignore())
            .ForMember(dest => dest.SeoDescriptionRu, opt => opt.Ignore())
            .ForMember(dest => dest.SeoKeywordsAz, opt => opt.Ignore())
            .ForMember(dest => dest.SeoKeywordsEn, opt => opt.Ignore())
            .ForMember(dest => dest.SeoKeywordsRu, opt => opt.Ignore());
    }
}