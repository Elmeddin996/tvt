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
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductSpecification> ProductSpecifications { get; set; }
    public DbSet<ProductDocument> ProductDocuments { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<ContactMessage> ContactMessages { get; set; }
    public DbSet<Subscriber> Subscribers { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<News> News { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
