using AutoMapper;
using TVT.Business.DTOs.Specifications;
using TVT.Core.Entities;

namespace TVT.Business.Mappings;

public class SpecificationProfile : Profile
{
    public SpecificationProfile()
    {
        CreateMap<Specification, SpecificationListDto>()
            .ForMember(x => x.SpecificationGroupName,
                opt => opt.MapFrom(src => src.SpecificationGroup.NameAz));

        CreateMap<Specification, SpecificationDetailDto>()
            .ForMember(x => x.SpecificationGroupName,
                opt => opt.MapFrom(src => src.SpecificationGroup.NameAz));

        CreateMap<CreateSpecificationDto, Specification>();

        CreateMap<UpdateSpecificationDto, Specification>();
    }
}
