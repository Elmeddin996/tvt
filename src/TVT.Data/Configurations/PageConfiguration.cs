using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TVT.Core.Entities;

namespace TVT.Data.Configurations;

public class PageConfiguration : IEntityTypeConfiguration<Page>
{
    public void Configure(EntityTypeBuilder<Page> builder)
    {
        builder.ToTable("Pages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TitleAz).IsRequired().HasMaxLength(255);
        builder.Property(x => x.TitleEn).IsRequired().HasMaxLength(255);
        builder.Property(x => x.TitleRu).IsRequired().HasMaxLength(255);

        builder.Property(x => x.SlugAz).IsRequired().HasMaxLength(250);
        builder.Property(x => x.SlugEn).IsRequired().HasMaxLength(250);
        builder.Property(x => x.SlugRu).IsRequired().HasMaxLength(250);

        builder.Property(x => x.ContentAz).IsRequired();
        builder.Property(x => x.ContentEn).IsRequired();
        builder.Property(x => x.ContentRu).IsRequired();

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
