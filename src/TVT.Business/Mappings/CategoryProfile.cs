using AutoMapper;
using TVT.Business.DTOs.Categories;
using TVT.Core.Entities;

namespace TVT.Business.Mappings;

public sealed class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryListDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameAz))
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.SlugAz));

        CreateMap<Category, CategoryDetailDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameAz))
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.SlugAz))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.DescriptionAz));

        CreateMap<CreateCategoryDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Parent, opt => opt.Ignore())
            .ForMember(dest => dest.Children, opt => opt.Ignore());

        CreateMap<UpdateCategoryDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Parent, opt => opt.Ignore())
            .ForMember(dest => dest.Children, opt => opt.Ignore());
    }
}
