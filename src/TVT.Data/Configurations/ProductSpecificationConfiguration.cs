using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TVT.Core.Entities;

namespace TVT.Data.Configurations;

public class ProductSpecificationConfiguration : IEntityTypeConfiguration<ProductSpecification>
{
    public void Configure(EntityTypeBuilder<ProductSpecification> builder)
    {
        builder.ToTable("ProductSpecifications");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.NameAz).IsRequired().HasMaxLength(200);
        builder.Property(x => x.NameEn).IsRequired().HasMaxLength(200);
        builder.Property(x => x.NameRu).IsRequired().HasMaxLength(200);
        builder.Property(x => x.ValueAz).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.ValueEn).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.ValueRu).IsRequired().HasMaxLength(1000);

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
