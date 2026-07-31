using AutoMapper;
using TVT.Business.DTOs.SpecificationGroups;
using TVT.Core.Entities;

namespace TVT.Business.Mappings.Profiles;

public class SpecificationGroupProfile : Profile
{
    public SpecificationGroupProfile()
    {
        CreateMap<SpecificationGroup, SpecificationGroupListDto>();

        CreateMap<SpecificationGroup, SpecificationGroupDetailDto>();

        CreateMap<CreateSpecificationGroupDto, SpecificationGroup>();

        CreateMap<UpdateSpecificationGroupDto, SpecificationGroup>();

        CreateMap<SpecificationGroup, UpdateSpecificationGroupDto>();
    }
}
