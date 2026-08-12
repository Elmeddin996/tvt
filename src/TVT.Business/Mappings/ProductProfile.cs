using AutoMapper;
using TVT.Business.DTOs.ProductImages;
using TVT.Business.DTOs.ProductSpecifications;
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

        // ProductImage -> ProductImageDto
        CreateMap<ProductImage, ProductImageDto>();

        // ProductSpecification -> ProductSpecificationDto
        CreateMap<ProductSpecification, ProductSpecificationDto>()
            .ForMember(dest => dest.SpecificationId,
                opt => opt.MapFrom(src => src.SpecificationId))
            .ForMember(dest => dest.GroupName,
                opt => opt.MapFrom(src => src.Specification.SpecificationGroup.NameAz))
            .ForMember(dest => dest.SpecificationName,
                opt => opt.MapFrom(src => src.Specification.NameAz))
            .ForMember(dest => dest.ValueAz,
                opt => opt.MapFrom(src => src.ValueAz))
            .ForMember(dest => dest.ValueEn,
                opt => opt.MapFrom(src => src.ValueEn))
            .ForMember(dest => dest.ValueRu,
                opt => opt.MapFrom(src => src.ValueRu))
            .ForMember(dest => dest.DisplayOrder,
                opt => opt.MapFrom(src => src.Specification.DisplayOrder))
            .ForMember(dest => dest.GroupDisplayOrder,
                opt => opt.MapFrom(src => src.Specification.SpecificationGroup.DisplayOrder));

        // Entity -> Detail DTO
        CreateMap<Product, ProductDetailDto>()
            .ForMember(dest => dest.Images,
                opt => opt.MapFrom(src => src.ProductImages))
            .ForMember(dest => dest.Specifications,
                opt => opt.MapFrom(src => src.ProductSpecifications))
        .ForMember(dest => dest.BrandName,
        opt => opt.MapFrom(src => src.Brand.NameAz));

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
