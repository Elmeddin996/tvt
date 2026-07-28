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

        builder.Property(x => x.ValueAz)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.ValueEn)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.ValueRu)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne(x => x.Product)
            .WithMany(x => x.ProductSpecifications)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Specification)
            .WithMany(x => x.ProductSpecifications)
            .HasForeignKey(x => x.SpecificationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.ProductId,
            x.SpecificationId
        }).IsUnique();
    }
}
