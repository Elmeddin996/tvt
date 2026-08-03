using AutoMapper;
using TVT.Business.DTOs.SpecificationGroups;
using TVT.Core.Entities;

namespace TVT.Business.Mappings.Profiles;

public class SpecificationGroupProfile : Profile
{
    public SpecificationGroupProfile()
    {
        CreateMap<SpecificationGroup, SpecificationGroupListDto>()
            .ForMember(x => x.Categories,
                opt => opt.MapFrom(src =>
                    string.Join(", ",
                        src.CategorySpecificationGroups
                            .Select(x => x.Category.NameAz))));

        CreateMap<SpecificationGroup, SpecificationGroupDetailDto>()
            .ForMember(x => x.CategoryIds,
                opt => opt.MapFrom(src =>
                    src.CategorySpecificationGroups
                        .Select(x => x.CategoryId)));

        CreateMap<CreateSpecificationGroupDto, SpecificationGroup>();

        CreateMap<UpdateSpecificationGroupDto, SpecificationGroup>();

        CreateMap<SpecificationGroup, UpdateSpecificationGroupDto>()
            .ForMember(x => x.CategoryIds,
                opt => opt.MapFrom(src =>
                    src.CategorySpecificationGroups
                        .Select(x => x.CategoryId)));
    }
}
