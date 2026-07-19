using AutoMapper;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Brands;
using TVT.Core.Abstractions.UnitOfWork;
using TVT.Core.Entities;

namespace TVT.Business.Services;

public class BrandService : IBrandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<BrandListDto>> GetAllAsync()
    {
        var brands = await _unitOfWork.Brands.GetAllAsync();
        return _mapper.Map<List<BrandListDto>>(brands);
    }

    public async Task<BrandDetailDto?> GetByIdAsync(int id)
    {
        var brand = await _unitOfWork.Brands.GetByIdAsync(id);
        if (brand == null)
            return null;
        return _mapper.Map<BrandDetailDto>(brand);
    }

    public async Task<BrandDetailDto?> GetBySlugAsync(string slug)
    {
        var brand = await _unitOfWork.Brands.GetBySlugAsync(slug);
        if (brand == null)
            return null;
        return _mapper.Map<BrandDetailDto>(brand);
    }

    public async Task<int> CreateAsync(CreateBrandDto dto)
    {
        var brand = _mapper.Map<Brand>(dto);
        await _unitOfWork.Brands.AddAsync(brand);
        await _unitOfWork.SaveChangesAsync();
        return brand.Id;
    }

    public async Task UpdateAsync(UpdateBrandDto dto)
    {
        var brand = await _unitOfWork.Brands.GetByIdAsync(dto.Id);
        if (brand == null)
            throw new KeyNotFoundException("Brand not found.");
        _mapper.Map(dto, brand);
        await _unitOfWork.Brands.UpdateAsync(brand);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var brand = await _unitOfWork.Brands.GetByIdAsync(id);
        if (brand == null)
            throw new KeyNotFoundException("Brand not found.");
        await _unitOfWork.Brands.DeleteAsync(brand);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _unitOfWork.Brands.ExistsAsync(id);
    }
}