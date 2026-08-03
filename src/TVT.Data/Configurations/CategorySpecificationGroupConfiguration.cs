using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TVT.Core.Entities;

namespace TVT.Data.Configurations;

public class CategorySpecificationGroupConfiguration
    : IEntityTypeConfiguration<CategorySpecificationGroup>
{
    public void Configure(EntityTypeBuilder<CategorySpecificationGroup> builder)
    {
        builder.ToTable("CategorySpecificationGroups");

        builder.HasKey(x => new
        {
            x.CategoryId,
            x.SpecificationGroupId
        });

        builder.HasOne(x => x.Category)
            .WithMany(x => x.CategorySpecificationGroups)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.SpecificationGroup)
            .WithMany(x => x.CategorySpecificationGroups)
            .HasForeignKey(x => x.SpecificationGroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
