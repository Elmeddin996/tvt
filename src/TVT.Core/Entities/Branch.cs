namespace TVT.Core.Entities;

public class Branch : BaseEntity
{
    public string NameAz { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public string NameRu { get; set; } = null!;

    public string AddressAz { get; set; } = null!;
    public string AddressEn { get; set; } = null!;
    public string AddressRu { get; set; } = null!;

    public string Phone { get; set; } = null!;
    public string? Phone2 { get; set; }

    public string? WorkingHoursAz { get; set; }
    public string? WorkingHoursEn { get; set; }
    public string? WorkingHoursRu { get; set; }

    public string? GoogleMapsUrl { get; set; }

    public string? Image { get; set; }

    public string? DescriptionAz { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionRu { get; set; }

    public int DisplayOrder { get; set; }
}
