using Microsoft.EntityFrameworkCore;
using TVT.Core.Entities;

namespace TVT.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Brand> Brands { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductImage> ProductImages { get; set; }

    public DbSet<SpecificationGroup> SpecificationGroups { get; set; }

    public DbSet<Specification> Specifications { get; set; }

    public DbSet<ProductSpecification> ProductSpecifications { get; set; }

    public DbSet<Setting> Settings { get; set; }

    public DbSet<Slider> Sliders { get; set; }
    public DbSet<MiniSlider> MiniSliders { get; set; }
    public DbSet<MobileSlider> MobileSliders { get; set; } = null!;

    public DbSet<ContactMessage> ContactMessages { get; set; }

    public DbSet<Subscriber> Subscribers { get; set; }

    public DbSet<Page> Pages { get; set; }

    public DbSet<News> News { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Branch> Branches { get; set; }

    public DbSet<CategorySpecificationGroup> CategorySpecificationGroups => Set<CategorySpecificationGroup>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

}
