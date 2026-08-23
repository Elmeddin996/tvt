using AutoMapper;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.MiniSliders;
using TVT.Core.Abstractions.UnitOfWork;
using TVT.Core.Common.Pagination;
using TVT.Core.Entities;

namespace TVT.Business.Services;

public class MiniSliderService : IMiniSliderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MiniSliderService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResult<MiniSliderListDto>> GetPagedAsync(
        PagedRequest request)
    {
        var result = await _unitOfWork.MiniSliders
            .GetPagedAsync(request);

        return new PagedResult<MiniSliderListDto>
        {
            Items = _mapper.Map<IReadOnlyList<MiniSliderListDto>>(
                result.Items),

            CurrentPage = result.CurrentPage,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };
    }

    public async Task<IReadOnlyList<MiniSliderListDto>> GetActiveAsync()
    {
        var miniSliders =
            await _unitOfWork.MiniSliders.GetActiveAsync();

        return _mapper.Map<IReadOnlyList<MiniSliderListDto>>(
            miniSliders);
    }

    public async Task<MiniSliderDetailDto?> GetByIdAsync(int id)
    {
        var miniSlider =
            await _unitOfWork.MiniSliders.GetByIdAsync(id);

        if (miniSlider == null)
            return null;

        return _mapper.Map<MiniSliderDetailDto>(miniSlider);
    }

    public async Task<int> CreateAsync(CreateMiniSliderDto dto)
    {
        var miniSlider = _mapper.Map<MiniSlider>(dto);

        await _unitOfWork.MiniSliders.AddAsync(miniSlider);

        await _unitOfWork.SaveChangesAsync();

        return miniSlider.Id;
    }

    public async Task UpdateAsync(UpdateMiniSliderDto dto)
    {
        var miniSlider =
            await _unitOfWork.MiniSliders.GetByIdAsync(dto.Id);

        if (miniSlider == null)
            throw new KeyNotFoundException(
                $"Mini slider with ID {dto.Id} was not found.");

        _mapper.Map(dto, miniSlider);

        await _unitOfWork.MiniSliders.UpdateAsync(miniSlider);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var miniSlider =
            await _unitOfWork.MiniSliders.GetByIdAsync(id);

        if (miniSlider == null)
            throw new KeyNotFoundException(
                $"Mini slider with ID {id} was not found.");

        await _unitOfWork.MiniSliders.DeleteAsync(miniSlider);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _unitOfWork.MiniSliders.ExistsAsync(id);
    }
}
