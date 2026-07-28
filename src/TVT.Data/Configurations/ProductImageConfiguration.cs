using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TVT.Core.Entities;

namespace TVT.Data.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("ProductImages");

        builder.HasKey(x => x.Id);

        // Properties
        builder.Property(x => x.Image)
            .IsRequired()
            .HasMaxLength(500);

        // Relationships
        builder.HasOne(x => x.Product)
            .WithMany(x => x.ProductImages)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(x => x.ProductId);

        builder.HasIndex(x => new
        {
            x.ProductId,
            x.DisplayOrder
        });

        builder.HasIndex(x => new
        {
            x.ProductId,
            x.IsMain
        });
    }
}
