using TVT.Business.DTOs.SpecificationGroups;

namespace TVT.Business.Abstractions.Services;

public interface ISpecificationGroupService
{
    Task<List<SpecificationGroupListDto>> GetAllAsync();

    Task<SpecificationGroupDetailDto?> GetByIdAsync(int id);

    Task<int> CreateAsync(CreateSpecificationGroupDto dto);

    Task UpdateAsync(UpdateSpecificationGroupDto dto);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}
