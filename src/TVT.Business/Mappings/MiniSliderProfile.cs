using AutoMapper;
using TVT.Business.DTOs.MiniSliders;
using TVT.Core.Entities;

namespace TVT.Business.Mappings;

public sealed class MiniSliderProfile : Profile
{
    public MiniSliderProfile()
    {
        CreateMap<MiniSlider, MiniSliderListDto>();

        CreateMap<MiniSlider, MiniSliderDetailDto>();

        CreateMap<CreateMiniSliderDto, MiniSlider>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        CreateMap<UpdateMiniSliderDto, MiniSlider>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
    }
}
