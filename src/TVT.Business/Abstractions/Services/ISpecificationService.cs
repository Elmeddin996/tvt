using TVT.Business.DTOs.Specifications;

namespace TVT.Business.Abstractions.Services;

public interface ISpecificationService
{
    Task<List<SpecificationListDto>> GetAllAsync();

    Task<SpecificationDetailDto?> GetByIdAsync(int id);

    Task<int> CreateAsync(CreateSpecificationDto dto);

    Task UpdateAsync(UpdateSpecificationDto dto);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}
