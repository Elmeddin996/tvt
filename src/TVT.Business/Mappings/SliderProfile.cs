using AutoMapper;
using TVT.Business.DTOs.Sliders;
using TVT.Core.Entities;

namespace TVT.Business.Mappings;

public sealed class SliderProfile : Profile
{
    public SliderProfile()
    {
        CreateMap<Slider, SliderListDto>();

        CreateMap<Slider, SliderDetailDto>();

        CreateMap<CreateSliderDto, Slider>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        CreateMap<UpdateSliderDto, Slider>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
    }
}
