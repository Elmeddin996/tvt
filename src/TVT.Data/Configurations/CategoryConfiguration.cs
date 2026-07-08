using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TVT.Core.Entities;

namespace TVT.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.NameAz).IsRequired().HasMaxLength(200);
        builder.Property(x => x.NameEn).IsRequired().HasMaxLength(200);
        builder.Property(x => x.NameRu).IsRequired().HasMaxLength(200);
        builder.Property(x => x.SlugAz).IsRequired().HasMaxLength(200);
        builder.Property(x => x.SlugEn).IsRequired().HasMaxLength(200);
        builder.Property(x => x.SlugRu).IsRequired().HasMaxLength(200);

        builder.Property(x => x.DescriptionAz).HasMaxLength(2000);
        builder.Property(x => x.DescriptionEn).HasMaxLength(2000);
        builder.Property(x => x.DescriptionRu).HasMaxLength(2000);

        builder.Property(x => x.Image).HasMaxLength(500);
        builder.Property(x => x.Icon).HasMaxLength(500);

        builder.Property(x => x.SeoTitleAz).HasMaxLength(500);
        builder.Property(x => x.SeoTitleEn).HasMaxLength(500);
        builder.Property(x => x.SeoTitleRu).HasMaxLength(500);
        builder.Property(x => x.SeoDescriptionAz).HasMaxLength(500);
        builder.Property(x => x.SeoDescriptionEn).HasMaxLength(500);
        builder.Property(x => x.SeoDescriptionRu).HasMaxLength(500);
        builder.Property(x => x.SeoKeywordsAz).HasMaxLength(1000);
        builder.Property(x => x.SeoKeywordsEn).HasMaxLength(1000);
        builder.Property(x => x.SeoKeywordsRu).HasMaxLength(1000);

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
