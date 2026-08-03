using AutoMapper;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Settings;
using TVT.Core.Abstractions.UnitOfWork;
using TVT.Core.Entities;

namespace TVT.Business.Services;

public class SettingService : ISettingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SettingService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SettingDetailDto?> GetAsync()
    {
        var setting = await _unitOfWork.Settings.GetSettingAsync();

        if (setting is null)
        {
            setting = new Setting
            {
                CompanyName = "TVT"
            };

            await _unitOfWork.Settings.AddAsync(setting);
            await _unitOfWork.SaveChangesAsync();
        }

        return _mapper.Map<SettingDetailDto>(setting);
    }

    public async Task UpdateAsync(UpdateSettingDto dto)
    {
        var setting = await _unitOfWork.Settings.GetSettingAsync(); ;

        if (setting is null)
            throw new KeyNotFoundException("Settings not found.");

        _mapper.Map(dto, setting);

        setting.UpdatedDate = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
    }
}
