using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TVT.Core.Entities;

namespace TVT.Data.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.NameAz)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.NameEn)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.NameRu)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.AddressAz)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.AddressEn)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.AddressRu)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Phone)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Phone2)
            .HasMaxLength(50);

        builder.Property(x => x.WorkingHoursAz)
            .HasMaxLength(500);

        builder.Property(x => x.WorkingHoursEn)
            .HasMaxLength(500);

        builder.Property(x => x.WorkingHoursRu)
            .HasMaxLength(500);

        builder.Property(x => x.GoogleMapsUrl)
            .HasMaxLength(2000);

        builder.Property(x => x.Image)
            .HasMaxLength(500);

        builder.Property(x => x.DescriptionAz)
            .HasMaxLength(2000);

        builder.Property(x => x.DescriptionEn)
            .HasMaxLength(2000);

        builder.Property(x => x.DescriptionRu)
            .HasMaxLength(2000);

        builder.Property(x => x.DisplayOrder)
            .IsRequired()
            .HasDefaultValue(0);
    }
}
