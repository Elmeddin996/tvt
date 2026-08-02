using AutoMapper;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Specifications;
using TVT.Core.Abstractions.UnitOfWork;
using TVT.Core.Entities;

namespace TVT.Business.Services;

public class SpecificationService : ISpecificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SpecificationService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SpecificationListDto>> GetAllAsync()
    {
        var specifications = await _unitOfWork.Specifications.GetAllAsync();

        return _mapper.Map<List<SpecificationListDto>>(specifications);
    }

    public async Task<SpecificationDetailDto?> GetByIdAsync(int id)
    {
        var specification = await _unitOfWork.Specifications.GetByIdAsync(id);

        if (specification is null)
            return null;

        return _mapper.Map<SpecificationDetailDto>(specification);
    }

    public async Task<int> CreateAsync(CreateSpecificationDto dto)
    {
        var specificationGroup =
            await _unitOfWork.SpecificationGroups.GetByIdAsync(dto.SpecificationGroupId);

        if (specificationGroup is null)
            throw new KeyNotFoundException("Specification group not found.");

        var specification = _mapper.Map<Specification>(dto);

        await _unitOfWork.Specifications.AddAsync(specification);

        await _unitOfWork.SaveChangesAsync();

        return specification.Id;
    }

    public async Task UpdateAsync(UpdateSpecificationDto dto)
    {
        var specification =
            await _unitOfWork.Specifications.GetByIdAsync(dto.Id);

        if (specification is null)
            throw new KeyNotFoundException("Specification not found.");

        var specificationGroup =
            await _unitOfWork.SpecificationGroups.GetByIdAsync(dto.SpecificationGroupId);

        if (specificationGroup is null)
            throw new KeyNotFoundException("Specification group not found.");

        _mapper.Map(dto, specification);

        specification.UpdatedDate = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var specification =
            await _unitOfWork.Specifications.GetByIdAsync(id);

        if (specification is null)
            throw new KeyNotFoundException("Specification not found.");

        specification.IsDeleted = true;
        specification.IsActive = false;
        specification.UpdatedDate = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _unitOfWork.Specifications.ExistsAsync(id);
    }
}
