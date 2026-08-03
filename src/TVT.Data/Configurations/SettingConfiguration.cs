using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TVT.Core.Entities;

namespace TVT.Data.Configurations;

public class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.ToTable("Settings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CompanyName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Logo)
            .HasMaxLength(500);

        builder.Property(x => x.Phone1)
            .HasMaxLength(50);

        builder.Property(x => x.Phone2)
            .HasMaxLength(50);

        builder.Property(x => x.Email)
            .HasMaxLength(255);

        builder.Property(x => x.AddressAz)
            .HasMaxLength(500);

        builder.Property(x => x.AddressEn)
            .HasMaxLength(500);

        builder.Property(x => x.AddressRu)
            .HasMaxLength(500);

        builder.Property(x => x.WorkingHoursAz)
            .HasMaxLength(500);

        builder.Property(x => x.WorkingHoursEn)
            .HasMaxLength(500);

        builder.Property(x => x.WorkingHoursRu)
            .HasMaxLength(500);

        builder.Property(x => x.Facebook)
            .HasMaxLength(500);

        builder.Property(x => x.Instagram)
            .HasMaxLength(500);

        builder.Property(x => x.Youtube)
            .HasMaxLength(500);

        builder.Property(x => x.TikTok)
            .HasMaxLength(500);

        builder.Property(x => x.Telegram)
            .HasMaxLength(500);

        builder.Property(x => x.WhatsApp)
            .HasMaxLength(500);

        builder.Property(x => x.GoogleMap)
            .HasColumnType("text");

        builder.Property(x => x.FooterTextAz)
            .HasMaxLength(500);

        builder.Property(x => x.FooterTextEn)
            .HasMaxLength(500);

        builder.Property(x => x.FooterTextRu)
            .HasMaxLength(500);

        builder.Property(x => x.DefaultSeoTitleAz)
            .HasMaxLength(255);

        builder.Property(x => x.DefaultSeoTitleEn)
            .HasMaxLength(255);

        builder.Property(x => x.DefaultSeoTitleRu)
            .HasMaxLength(255);

        builder.Property(x => x.DefaultSeoDescriptionAz)
            .HasMaxLength(1000);

        builder.Property(x => x.DefaultSeoDescriptionEn)
            .HasMaxLength(1000);

        builder.Property(x => x.DefaultSeoDescriptionRu)
            .HasMaxLength(1000);

        builder.Property(x => x.DefaultSeoKeywordsAz)
            .HasMaxLength(1000);

        builder.Property(x => x.DefaultSeoKeywordsEn)
            .HasMaxLength(1000);

        builder.Property(x => x.DefaultSeoKeywordsRu)
            .HasMaxLength(1000);
    }
}
