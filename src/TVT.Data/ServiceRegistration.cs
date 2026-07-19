using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Abstractions.UnitOfWork;
using TVT.Data.Context;
using TVT.Data.Repositories;

namespace TVT.Data;

public static class ServiceRegistration
{
    public static IServiceCollection AddDataServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Generic Repository
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        // Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<INewsRepository, NewsRepository>();
        services.AddScoped<IPageRepository, PageRepository>();
        services.AddScoped<ISliderRepository, SliderRepository>();
        services.AddScoped<ISettingRepository, SettingRepository>();
        services.AddScoped<ISubscriberRepository, SubscriberRepository>();
        services.AddScoped<IContactMessageRepository, ContactMessageRepository>();

        // Unit of Work
        services.AddScoped<IUnitOfWork, TVT.Data.UnitOfWork.UnitOfWork>();

        return services;
    }
}