using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TVT.Core.Entities;

namespace TVT.Data.Configurations;

public class SpecificationConfiguration : IEntityTypeConfiguration<Specification>
{
    public void Configure(EntityTypeBuilder<Specification> builder)
    {
        builder.ToTable("Specifications");

        builder.HasKey(x => x.Id);

        // Properties
        builder.Property(x => x.NameAz)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.NameEn)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.NameRu)
            .IsRequired()
            .HasMaxLength(200);

        // Relationships
        builder.HasOne(x => x.SpecificationGroup)
     .WithMany(x => x.Specifications)
     .HasForeignKey(x => x.SpecificationGroupId)
     .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(x => x.SpecificationGroupId);

        builder.HasIndex(x => new
        {
            x.SpecificationGroupId,
            x.DisplayOrder
        });

        builder.HasIndex(x => new
        {
            x.SpecificationGroupId,
            x.NameAz
        }).IsUnique();

        builder.HasIndex(x => new
        {
            x.SpecificationGroupId,
            x.NameEn
        }).IsUnique();

        builder.HasIndex(x => new
        {
            x.SpecificationGroupId,
            x.NameRu
        }).IsUnique();
    }
}
