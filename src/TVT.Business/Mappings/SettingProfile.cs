using AutoMapper;
using TVT.Business.DTOs.Settings;
using TVT.Core.Entities;

namespace TVT.Business.Mappings;

public sealed class SettingProfile : Profile
{
    public SettingProfile()
    {
        CreateMap<Setting, SettingDto>();

        CreateMap<UpdateSettingDto, Setting>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
    }
}
