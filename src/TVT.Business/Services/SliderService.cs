using AutoMapper;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Sliders;
using TVT.Core.Abstractions;
using TVT.Core.Abstractions.UnitOfWork;
using TVT.Core.Common.Pagination;
using TVT.Core.Entities;

namespace TVT.Business.Services;

public class SliderService : ISliderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SliderService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResult<SliderListDto>> GetPagedAsync(PagedRequest request)
    {
        var result = await _unitOfWork.Sliders.GetPagedAsync(request);

        return new PagedResult<SliderListDto>
        {
            Items = _mapper.Map<IReadOnlyList<SliderListDto>>(result.Items),
            CurrentPage = result.CurrentPage,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };
    }

    public async Task<IReadOnlyList<SliderDetailDto>> GetActiveSlidersAsync()
    {
        var sliders = await _unitOfWork.Sliders.GetActiveSlidersAsync();

        return _mapper.Map<IReadOnlyList<SliderDetailDto>>(sliders);
    }

    public async Task<SliderDetailDto?> GetByIdAsync(int id)
    {
        var slider = await _unitOfWork.Sliders.GetByIdAsync(id);

        if (slider == null)
            return null;

        return _mapper.Map<SliderDetailDto>(slider);
    }

    public async Task<int> CreateAsync(CreateSliderDto dto)
    {
        var slider = _mapper.Map<Slider>(dto);

        await _unitOfWork.Sliders.AddAsync(slider);

        await _unitOfWork.SaveChangesAsync();

        return slider.Id;
    }

    public async Task UpdateAsync(UpdateSliderDto dto)
    {
        var slider = await _unitOfWork.Sliders.GetByIdAsync(dto.Id);

        if (slider == null)
            throw new KeyNotFoundException($"Slider with ID {dto.Id} was not found.");

        _mapper.Map(dto, slider);

        await _unitOfWork.Sliders.UpdateAsync(slider);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var slider = await _unitOfWork.Sliders.GetByIdAsync(id);

        if (slider == null)
            throw new KeyNotFoundException($"Slider with ID {id} was not found.");

        await _unitOfWork.Sliders.DeleteAsync(slider);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _unitOfWork.Sliders.ExistsAsync(id);
    }
}
