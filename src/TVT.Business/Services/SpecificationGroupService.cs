using AutoMapper;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.SpecificationGroups;
using TVT.Core.Abstractions.UnitOfWork;
using TVT.Core.Entities;

namespace TVT.Business.Services;

public class SpecificationGroupService : ISpecificationGroupService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SpecificationGroupService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SpecificationGroupListDto>> GetAllAsync()
    {
        var groups = await _unitOfWork.SpecificationGroups.GetAllAsync();

        return _mapper.Map<List<SpecificationGroupListDto>>(groups);
    }

    public async Task<SpecificationGroupDetailDto?> GetByIdAsync(int id)
    {
        var group = await _unitOfWork.SpecificationGroups.GetByIdAsync(id);

        if (group is null)
            return null;

        return _mapper.Map<SpecificationGroupDetailDto>(group);
    }

    public async Task<int> CreateAsync(CreateSpecificationGroupDto dto)
    {
        var group = _mapper.Map<SpecificationGroup>(dto);

        await _unitOfWork.SpecificationGroups.AddAsync(group);

        foreach (var categoryId in dto.CategoryIds)
        {
            group.CategorySpecificationGroups.Add(new CategorySpecificationGroup
            {
                CategoryId = categoryId
            });
        }

        await _unitOfWork.SaveChangesAsync();

        return group.Id;
    }


    public async Task UpdateAsync(UpdateSpecificationGroupDto dto)
    {
        var group = await _unitOfWork.SpecificationGroups.GetByIdAsync(dto.Id);

        if (group is null)
            throw new KeyNotFoundException("Specification group not found.");

        _mapper.Map(dto, group);

        group.CategorySpecificationGroups.Clear();

        foreach (var categoryId in dto.CategoryIds)
        {
            group.CategorySpecificationGroups.Add(new CategorySpecificationGroup
            {
                CategoryId = categoryId,
                SpecificationGroupId = group.Id
            });
        }

        group.UpdatedDate = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var group = await _unitOfWork.SpecificationGroups.GetByIdAsync(id);

        if (group is null)
            throw new KeyNotFoundException("Specification group not found.");

        group.IsDeleted = true;
        group.IsActive = false;
        group.UpdatedDate = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _unitOfWork.SpecificationGroups.ExistsAsync(id);
    }
}
