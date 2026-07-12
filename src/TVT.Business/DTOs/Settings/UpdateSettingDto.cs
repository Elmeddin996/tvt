namespace TVT.Business.DTOs.Settings;

public class UpdateSettingDto
{
    public string CompanyName { get; set; } = null!;
    public string? Logo { get; set; }
    public string? Phone1 { get; set; }
    public string? Phone2 { get; set; }
    public string? Email { get; set; }
    public string? AddressAz { get; set; }
    public string? AddressEn { get; set; }
    public string? AddressRu { get; set; }
    public string? WorkingHoursAz { get; set; }
    public string? WorkingHoursEn { get; set; }
    public string? WorkingHoursRu { get; set; }
    public string? Facebook { get; set; }
    public string? Instagram { get; set; }
    public string? Youtube { get; set; }
    public string? Linkedin { get; set; }
    public string? GoogleMap { get; set; }
}
