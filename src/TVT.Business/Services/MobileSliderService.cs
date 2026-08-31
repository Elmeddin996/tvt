using AutoMapper;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.MobileSliders;
using TVT.Core.Abstractions.UnitOfWork;
using TVT.Core.Common.Pagination;
using TVT.Core.Entities;

namespace TVT.Business.Services;

public class MobileSliderService : IMobileSliderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MobileSliderService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResult<MobileSliderListDto>> GetPagedAsync(
        PagedRequest request)
    {
        var result = await _unitOfWork.MobileSliders
            .GetPagedAsync(request);

        return new PagedResult<MobileSliderListDto>
        {
            Items = _mapper.Map<IReadOnlyList<MobileSliderListDto>>(
                result.Items),

            CurrentPage = result.CurrentPage,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };
    }

    public async Task<IReadOnlyList<MobileSliderListDto>> GetActiveAsync()
    {
        var mobileSliders =
            await _unitOfWork.MobileSliders.GetActiveAsync();

        return _mapper.Map<IReadOnlyList<MobileSliderListDto>>(
            mobileSliders);
    }

    public async Task<MobileSliderDetailDto?> GetByIdAsync(int id)
    {
        var mobileSlider =
            await _unitOfWork.MobileSliders.GetByIdAsync(id);

        if (mobileSlider == null)
            return null;

        return _mapper.Map<MobileSliderDetailDto>(mobileSlider);
    }

    public async Task<int> CreateAsync(CreateMobileSliderDto dto)
    {
        var mobileSlider = _mapper.Map<MobileSlider>(dto);

        await _unitOfWork.MobileSliders.AddAsync(mobileSlider);
        await _unitOfWork.SaveChangesAsync();

        return mobileSlider.Id;
    }

    public async Task UpdateAsync(UpdateMobileSliderDto dto)
    {
        var mobileSlider =
            await _unitOfWork.MobileSliders.GetByIdAsync(dto.Id);

        if (mobileSlider == null)
            throw new KeyNotFoundException(
                $"Mobile slider with ID {dto.Id} was not found.");

        _mapper.Map(dto, mobileSlider);

        await _unitOfWork.MobileSliders.UpdateAsync(mobileSlider);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var mobileSlider =
            await _unitOfWork.MobileSliders.GetByIdAsync(id);

        if (mobileSlider == null)
            throw new KeyNotFoundException(
                $"Mobile slider with ID {id} was not found.");

        await _unitOfWork.MobileSliders.DeleteAsync(mobileSlider);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _unitOfWork.MobileSliders.ExistsAsync(id);
    }
}
