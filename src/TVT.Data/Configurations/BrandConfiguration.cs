using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TVT.Core.Entities;

namespace TVT.Data.Configurations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.NameAz).IsRequired().HasMaxLength(200);
        builder.Property(x => x.NameEn).IsRequired().HasMaxLength(200);
        builder.Property(x => x.NameRu).IsRequired().HasMaxLength(200);
        builder.Property(x => x.SlugAz).IsRequired().HasMaxLength(250);
        builder.Property(x => x.SlugEn).IsRequired().HasMaxLength(250);
        builder.Property(x => x.SlugRu).IsRequired().HasMaxLength(250);

        builder.Property(x => x.DescriptionAz).HasMaxLength(4000);
        builder.Property(x => x.DescriptionEn).HasMaxLength(4000);
        builder.Property(x => x.DescriptionRu).HasMaxLength(4000);

        builder.Property(x => x.Logo).HasMaxLength(500);
        builder.Property(x => x.Website).HasMaxLength(500);

        builder.Property(x => x.SeoTitleAz).HasMaxLength(255);
        builder.Property(x => x.SeoTitleEn).HasMaxLength(255);
        builder.Property(x => x.SeoTitleRu).HasMaxLength(255);
        builder.Property(x => x.SeoDescriptionAz).HasMaxLength(500);
        builder.Property(x => x.SeoDescriptionEn).HasMaxLength(500);
        builder.Property(x => x.SeoDescriptionRu).HasMaxLength(500);
        builder.Property(x => x.SeoKeywordsAz).HasMaxLength(1000);
        builder.Property(x => x.SeoKeywordsEn).HasMaxLength(1000);
        builder.Property(x => x.SeoKeywordsRu).HasMaxLength(1000);
    }
}
