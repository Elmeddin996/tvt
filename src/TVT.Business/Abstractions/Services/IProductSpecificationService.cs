using TVT.Business.DTOs.ProductSpecifications;

namespace TVT.Business.Abstractions.Services;

public interface IProductSpecificationService
{
    Task<List<ProductSpecificationDto>> GetByProductIdAsync(int productId);

    Task UpdateAsync(
        int productId,
        List<UpdateProductSpecificationDto> specifications);
}
