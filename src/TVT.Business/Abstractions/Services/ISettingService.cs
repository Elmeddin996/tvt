using TVT.Business.DTOs.Settings;

namespace TVT.Business.Abstractions.Services;

public interface ISettingService
{
    Task<SettingDetailDto?> GetAsync();

    Task UpdateAsync(UpdateSettingDto dto);
}
