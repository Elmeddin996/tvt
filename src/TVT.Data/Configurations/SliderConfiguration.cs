using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TVT.Core.Entities;

namespace TVT.Data.Configurations;

public class SliderConfiguration : IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> builder)
    {
        builder.ToTable("Sliders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TitleAz).HasMaxLength(255);
        builder.Property(x => x.TitleEn).HasMaxLength(255);
        builder.Property(x => x.TitleRu).HasMaxLength(255);

        builder.Property(x => x.DescriptionAz).HasMaxLength(4000);
        builder.Property(x => x.DescriptionEn).HasMaxLength(4000);
        builder.Property(x => x.DescriptionRu).HasMaxLength(4000);

        builder.Property(x => x.Image).IsRequired().HasMaxLength(500);

        builder.Property(x => x.ButtonTextAz).HasMaxLength(100);
        builder.Property(x => x.ButtonTextEn).HasMaxLength(100);
        builder.Property(x => x.ButtonTextRu).HasMaxLength(100);

        builder.Property(x => x.ButtonLink).HasMaxLength(500);
    }
}
