using AutoMapper;
using TVT.Business.DTOs.MobileSliders;
using TVT.Core.Entities;

namespace TVT.Business.Mappings;

public sealed class MobileSliderProfile : Profile
{
    public MobileSliderProfile()
    {
        CreateMap<MobileSlider, MobileSliderListDto>();
        CreateMap<MobileSlider, MobileSliderDetailDto>();

        CreateMap<CreateMobileSliderDto, MobileSlider>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate,
                opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate,
                opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted,
                opt => opt.Ignore());

        CreateMap<UpdateMobileSliderDto, MobileSlider>()
            .ForMember(dest => dest.CreatedDate,
                opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate,
                opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted,
                opt => opt.Ignore());
    }
}
