using AutoMapper;
using TVT.Business.DTOs.Categories;
using TVT.Core.Entities;

namespace TVT.Business.Mappings;

public sealed class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        // Entity -> List DTO
        CreateMap<Category, CategoryListDto>()
    .ForMember(dest => dest.ParentName,
        opt => opt.MapFrom(src => src.Parent != null ? src.Parent.NameAz : null));
        // Entity -> Detail DTO
        CreateMap<Category, CategoryDetailDto>();

        // Entity -> Update DTO
        CreateMap<Category, UpdateCategoryDto>();

        // Create DTO -> Entity
        CreateMap<CreateCategoryDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Parent, opt => opt.Ignore())
            .ForMember(dest => dest.Children, opt => opt.Ignore());

        // Update DTO -> Entity
        CreateMap<UpdateCategoryDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Parent, opt => opt.Ignore())
            .ForMember(dest => dest.Children, opt => opt.Ignore());
    }
}
