using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TVT.Core.Entities;

namespace TVT.Data.Configurations;

public class MobileSliderConfiguration : IEntityTypeConfiguration<MobileSlider>
{
    public void Configure(EntityTypeBuilder<MobileSlider> builder)
    {
        builder.ToTable("MobileSliders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Image)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Link)
            .HasMaxLength(1000);

        builder.Property(x => x.SortOrder)
            .IsRequired();
    }
}
