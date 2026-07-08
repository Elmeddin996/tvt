using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TVT.Core.Entities;

namespace TVT.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Model).IsRequired().HasMaxLength(200);

        builder.Property(x => x.NameAz).IsRequired().HasMaxLength(200);
        builder.Property(x => x.NameEn).IsRequired().HasMaxLength(200);
        builder.Property(x => x.NameRu).IsRequired().HasMaxLength(200);
        builder.Property(x => x.SlugAz).IsRequired().HasMaxLength(250);
        builder.Property(x => x.SlugEn).IsRequired().HasMaxLength(250);
        builder.Property(x => x.SlugRu).IsRequired().HasMaxLength(250);

        builder.Property(x => x.DescriptionAz).HasMaxLength(4000);
        builder.Property(x => x.DescriptionEn).HasMaxLength(4000);
        builder.Property(x => x.DescriptionRu).HasMaxLength(4000);

        builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        builder.Property(x => x.OldPrice).HasColumnType("decimal(18,2)");

        builder.Property(x => x.SeoTitleAz).HasMaxLength(255);
        builder.Property(x => x.SeoTitleEn).HasMaxLength(255);
        builder.Property(x => x.SeoTitleRu).HasMaxLength(255);
        builder.Property(x => x.SeoDescriptionAz).HasMaxLength(500);
        builder.Property(x => x.SeoDescriptionEn).HasMaxLength(500);
        builder.Property(x => x.SeoDescriptionRu).HasMaxLength(500);
        builder.Property(x => x.SeoKeywordsAz).HasMaxLength(1000);
        builder.Property(x => x.SeoKeywordsEn).HasMaxLength(1000);
        builder.Property(x => x.SeoKeywordsRu).HasMaxLength(1000);

        builder.HasOne(x => x.Brand)
            .WithMany()
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
