using AutoMapper;
using TVT.Business.DTOs.ContactMessages;
using TVT.Core.Entities;

namespace TVT.Business.Mappings;

public sealed class ContactMessageProfile : Profile
{
    public ContactMessageProfile()
    {
        CreateMap<ContactMessage, ContactMessageDto>();

        CreateMap<UpdateContactMessageDto, ContactMessage>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
    }
}
