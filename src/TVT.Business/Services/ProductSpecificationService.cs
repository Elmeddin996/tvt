using AutoMapper;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.ProductSpecifications;
using TVT.Core.Abstractions.UnitOfWork;
using TVT.Core.Entities;

namespace TVT.Business.Services;

public class ProductSpecificationService : IProductSpecificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductSpecificationService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ProductSpecificationDto>> GetByProductIdAsync(int productId)
    {
        var specifications =
            await _unitOfWork.Specifications.GetForProductAsync(productId);

        return specifications
            .Select(specification => new ProductSpecificationDto
            {
                SpecificationId = specification.Id,

                GroupName = specification.SpecificationGroup.NameAz,

                GroupDisplayOrder = specification.SpecificationGroup.DisplayOrder,

                SpecificationName = specification.NameAz,

                DisplayOrder = specification.DisplayOrder,

                ValueAz = specification.ProductSpecifications
                    .FirstOrDefault()?.ValueAz,

                ValueEn = specification.ProductSpecifications
                    .FirstOrDefault()?.ValueEn,

                ValueRu = specification.ProductSpecifications
                    .FirstOrDefault()?.ValueRu

            })
            .OrderBy(x => x.GroupDisplayOrder)
            .ThenBy(x => x.DisplayOrder)
            .ToList();
    }
    public async Task UpdateAsync(
    int productId,
    List<UpdateProductSpecificationDto> specifications)
    {
        var currentSpecifications =
            await _unitOfWork.ProductSpecifications.GetByProductIdAsync(productId);

        if (currentSpecifications.Any())
        {
            await _unitOfWork.ProductSpecifications.DeleteRangeAsync(currentSpecifications);
        }

        foreach (var specification in specifications)
        {
            if (string.IsNullOrWhiteSpace(specification.ValueAz) &&
                string.IsNullOrWhiteSpace(specification.ValueEn) &&
                string.IsNullOrWhiteSpace(specification.ValueRu))
            {
                continue;
            }

            await _unitOfWork.ProductSpecifications.AddAsync(new ProductSpecification
            {
                ProductId = productId,
                SpecificationId = specification.SpecificationId,
                ValueAz = specification.ValueAz ?? string.Empty,
                ValueEn = specification.ValueEn ?? string.Empty,
                ValueRu = specification.ValueRu ?? string.Empty
            });
        }

        await _unitOfWork.SaveChangesAsync();
    }
}
