using AutoMapper;
using TVT.Business.DTOs.Branches;
using TVT.Core.Entities;

namespace TVT.Business.Mappings;

public sealed class BranchProfile : Profile
{
    public BranchProfile()
    {
        // Entity -> List DTO
        CreateMap<Branch, BranchListDto>();

        // Entity -> Update DTO
        CreateMap<Branch, UpdateBranchDto>();

        // Create DTO -> Entity
        CreateMap<CreateBranchDto, Branch>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        // Update DTO -> Entity
        CreateMap<UpdateBranchDto, Branch>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
    }
}
