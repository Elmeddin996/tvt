using AutoMapper;
using TVT.Core.Common.Pagination;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Categories;
using TVT.Core.Abstractions.UnitOfWork;
using TVT.Core.Entities;

namespace TVT.Business.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<CategoryListDto>> GetAllAsync()
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();
        return _mapper.Map<List<CategoryListDto>>(categories);
    }

    public async Task<PagedResult<CategoryListDto>> GetPagedAsync(PagedRequest request)
    {
        var result = await _unitOfWork.Categories.GetPagedAsync(request);

        return new PagedResult<CategoryListDto>
        {
            Items = _mapper.Map<List<CategoryListDto>>(result.Items),
            CurrentPage = result.CurrentPage,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };
    }

    public async Task<CategoryDetailDto?> GetByIdAsync(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);

        if (category == null)
            return null;

        return _mapper.Map<CategoryDetailDto>(category);
    }

    public async Task<CategoryDetailDto?> GetBySlugAsync(string slug)
    {
        var category = await _unitOfWork.Categories.GetBySlugAsync(slug);

        if (category == null)
            return null;

        return _mapper.Map<CategoryDetailDto>(category);
    }

    public async Task<int> CreateAsync(CreateCategoryDto dto)
    {
        var category = _mapper.Map<Category>(dto);

        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();

        return category.Id;
    }

    public async Task UpdateAsync(UpdateCategoryDto dto)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(dto.Id);

        if (category == null)
            throw new KeyNotFoundException("Category not found.");

        if (dto.ParentId.HasValue)
        {
            if (dto.ParentId.Value == dto.Id)
                throw new InvalidOperationException(
                    "A category cannot be its own parent.");

            var categories = await _unitOfWork.Categories.GetAllAsync();

            if (IsDescendant(categories, dto.Id, dto.ParentId.Value))
                throw new InvalidOperationException(
                    "A category cannot be assigned to one of its descendants.");
        }

        _mapper.Map(dto, category);

        category.UpdatedDate = DateTime.UtcNow;

        await _unitOfWork.Categories.UpdateAsync(category);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);

        if (category == null)
            throw new KeyNotFoundException("Category not found.");

        var hasSubCategories = await _unitOfWork.Categories.HasSubCategoriesAsync(id);

        if (hasSubCategories)
            throw new InvalidOperationException(
                "This category cannot be deleted because it contains subcategories.");

        await _unitOfWork.Categories.DeleteAsync(category);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _unitOfWork.Categories.ExistsAsync(id);
    }

    public async Task<List<CategoryListDto>> GetParentCategoriesAsync(int? excludeCategoryId = null)
    {
        var categories = await _unitOfWork.Categories.GetAllActiveAsync();

        if (excludeCategoryId.HasValue)
        {
            categories = categories
                .Where(x => x.Id != excludeCategoryId.Value)
                .ToList();
        }

        return _mapper.Map<List<CategoryListDto>>(categories);
    }

    private static bool IsDescendant(
    List<Category> categories,
    int categoryId,
    int parentId)
    {
        var children = categories
            .Where(x => x.ParentId == categoryId)
            .ToList();

        foreach (var child in children)
        {
            if (child.Id == parentId)
                return true;

            if (IsDescendant(categories, child.Id, parentId))
                return true;
        }

        return false;
    }
}
