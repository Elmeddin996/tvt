using AutoMapper;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Products;
using TVT.Business.Helpers;
using TVT.Core.Abstractions.UnitOfWork;
using TVT.Core.Common.Filters;
using TVT.Core.Common.Pagination;
using TVT.Core.Entities;

namespace TVT.Business.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ProductListDto>> GetAllAsync()
    {
        var products = await _unitOfWork.Products.GetAllAsync();

        return _mapper.Map<List<ProductListDto>>(products);
    }

    public async Task<ProductDetailDto?> GetByIdAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if (product is null)
            return null;

        return _mapper.Map<ProductDetailDto>(product);
    }

    public async Task<ProductDetailDto?> GetBySlugAsync(string slug)
    {
        var product = await _unitOfWork.Products.GetBySlugAsync(slug);

        if (product is null)
            return null;

        return _mapper.Map<ProductDetailDto>(product);
    }

    public async Task<int> CreateAsync(CreateProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);

        product.SlugAz = $"{SlugHelper.Generate(product.NameAz)}-{SlugHelper.Generate(product.Code)}";
        product.SlugEn = $"{SlugHelper.Generate(product.NameEn)}-{SlugHelper.Generate(product.Code)}";
        product.SlugRu = $"{SlugHelper.Generate(product.NameRu)}-{SlugHelper.Generate(product.Code)}";

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return product.Id;
    }

    public async Task UpdateAsync(UpdateProductDto dto)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(dto.Id);

        if (product is null)
            throw new KeyNotFoundException("Product not found.");

        _mapper.Map(dto, product);

        product.SlugAz = $"{SlugHelper.Generate(product.NameAz)}-{SlugHelper.Generate(product.Code)}";
        product.SlugEn = $"{SlugHelper.Generate(product.NameEn)}-{SlugHelper.Generate(product.Code)}";
        product.SlugRu = $"{SlugHelper.Generate(product.NameRu)}-{SlugHelper.Generate(product.Code)}";

        product.UpdatedDate = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if (product is null)
            throw new KeyNotFoundException("Product not found.");

        if (product.ProductImages.Any())
        {
            await _unitOfWork.ProductImages.DeleteRangeAsync(product.ProductImages);
        }

        product.IsDeleted = true;
        product.IsActive = false;
        product.UpdatedDate = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _unitOfWork.Products.ExistsAsync(id);
    }

    public async Task<PagedResult<ProductListDto>> SearchAsync(
      string search,
      int? categoryId,
      PagedRequest request)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return new PagedResult<ProductListDto>
            {
                CurrentPage = request.Page,
                PageSize = request.PageSize
            };
        }

        var result = await _unitOfWork.Products.SearchAsync(
            search,
            categoryId,
            request);

        return new PagedResult<ProductListDto>
        {
            Items = _mapper.Map<List<ProductListDto>>(result.Items),
            CurrentPage = result.CurrentPage,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };
    }

    public async Task<PagedResult<ProductListDto>> GetByCategoryIdsAsync(
    IReadOnlyCollection<int> categoryIds,
    ProductFilterRequest filter,
    PagedRequest request)
    {
        var result = await _unitOfWork.Products.GetByCategoryIdsAsync(
            categoryIds,
            filter,
            request);

        return new PagedResult<ProductListDto>
        {
            Items = _mapper.Map<List<ProductListDto>>(result.Items),
            CurrentPage = result.CurrentPage,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };
    }

    public async Task<ProductFilterOptionsDto> GetFilterOptionsAsync(
    IReadOnlyCollection<int> categoryIds)
    {
        var result = await _unitOfWork.Products.GetFilterOptionsAsync(
            categoryIds);

        return new ProductFilterOptionsDto
        {
            MinPrice = result.MinPrice,
            MaxPrice = result.MaxPrice,
            InStockCount = result.InStockCount,
            OutOfStockCount = result.OutOfStockCount,

            Brands = result.Brands
                .Select(x => new ProductFilterBrandDto
                {
                    BrandId = x.BrandId,
                    Name = x.Name,
                    ProductCount = x.ProductCount
                })
                .ToList()
        };
    }
}
