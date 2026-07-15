using AutoMapper;
using TVT.Business.DTOs.Brands;
using TVT.Core.Entities;

namespace TVT.Business.Mappings;

public sealed class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandListDto>();

        CreateMap<Brand, BrandDetailDto>();

        CreateMap<Brand, BrandLookupDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameAz));

        CreateMap<CreateBrandDto, Brand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        CreateMap<UpdateBrandDto, Brand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
    }
}
