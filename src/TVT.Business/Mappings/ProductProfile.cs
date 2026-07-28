using AutoMapper;
using TVT.Business.DTOs.Products;
using TVT.Core.Entities;

namespace TVT.Business.Mappings;

public sealed class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Entity -> List DTO
        CreateMap<Product, ProductListDto>()
            .ForMember(dest => dest.NameAz,
                opt => opt.MapFrom(src => src.NameAz))
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.NameAz))
            .ForMember(dest => dest.BrandName,
                opt => opt.MapFrom(src => src.Brand.NameAz))
            .ForMember(dest => dest.MainImage,
                opt => opt.MapFrom(src =>
                    src.ProductImages
                        .Where(x => x.IsMain)
                        .Select(x => x.Image)
                        .FirstOrDefault()));

        // Entity -> Detail DTO
        CreateMap<Product, ProductDetailDto>();

        // Create DTO -> Entity
        CreateMap<CreateProductDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Brand, opt => opt.Ignore())
            .ForMember(dest => dest.ProductImages, opt => opt.Ignore())
            .ForMember(dest => dest.ProductSpecifications, opt => opt.Ignore());

        // Update DTO -> Entity
        CreateMap<UpdateProductDto, Product>()
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Brand, opt => opt.Ignore())
            .ForMember(dest => dest.ProductImages, opt => opt.Ignore())
            .ForMember(dest => dest.ProductSpecifications, opt => opt.Ignore());
    }
}
