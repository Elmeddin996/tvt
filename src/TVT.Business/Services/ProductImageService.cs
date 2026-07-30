using AutoMapper;
using TVT.Business.DTOs.ProductImages;
using TVT.Core.Abstractions.UnitOfWork;
using TVT.Core.Entities;

namespace TVT.Business.Services;

public class ProductImageService : IProductImageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductImageService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ProductImageDto>> GetByProductIdAsync(int productId)
    {
        var images = await _unitOfWork.ProductImages.GetByProductIdAsync(productId);

        return _mapper.Map<List<ProductImageDto>>(images);
    }

    public async Task UploadAsync(UploadProductImageDto dto)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(dto.ProductId);

        if (product is null)
            throw new KeyNotFoundException("Product not found.");

        var hasMainImage = await _unitOfWork.ProductImages.ExistsMainImageAsync(dto.ProductId);

        if (!hasMainImage)
        {
            dto.IsMain = true;
        }

        if (dto.IsMain)
        {
            var currentMainImage = product.ProductImages.FirstOrDefault(x => x.IsMain);

            if (currentMainImage is not null)
            {
                currentMainImage.IsMain = false;
            }
        }

        var productImage = new ProductImage
        {
            ProductId = dto.ProductId,
            Image = dto.Image,
            IsMain = dto.IsMain,
            DisplayOrder = dto.DisplayOrder
        };

        await _unitOfWork.ProductImages.AddAsync(productImage);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int imageId)
    {
        var image = await _unitOfWork.ProductImages.GetByIdAsync(imageId);

        if (image is null)
            throw new KeyNotFoundException("Image not found.");

        await _unitOfWork.ProductImages.DeleteAsync(image);

        await _unitOfWork.SaveChangesAsync();
    }


    public async Task SetMainImageAsync(int imageId)
    {
        var image = await _unitOfWork.ProductImages.GetByIdAsync(imageId);

        if (image is null)
            throw new KeyNotFoundException("Image not found.");

        var currentMainImage = await _unitOfWork.ProductImages.GetMainImageAsync(image.ProductId);

        if (currentMainImage is not null && currentMainImage.Id != image.Id)
        {
            currentMainImage.IsMain = false;

            await _unitOfWork.ProductImages.UpdateAsync(currentMainImage);
        }

        image.IsMain = true;

        await _unitOfWork.ProductImages.UpdateAsync(image);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateDisplayOrderAsync(List<UpdateProductImageOrderDto> dto)
    {
        throw new NotImplementedException();
    }


    public async Task<ProductImageDto?> GetByIdAsync(int imageId)
    {
        var image = await _unitOfWork.ProductImages.GetByIdAsync(imageId);

        if (image is null)
            return null;

        return _mapper.Map<ProductImageDto>(image);
    }
}
