using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TVT.Business.Abstractions.Services;
using TVT.Business.Services;

namespace TVT.Business;

public static class ServiceRegistration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(ServiceRegistration).Assembly);
        services.AddValidatorsFromAssembly(typeof(ServiceRegistration).Assembly);

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<ISliderService, SliderService>();
        services.AddScoped<IMiniSliderService, MiniSliderService>();
        services.AddScoped<IMobileSliderService, MobileSliderService>();
        services.AddScoped<IProductImageService, ProductImageService>();
        services.AddScoped<ISpecificationGroupService, SpecificationGroupService>();
        services.AddScoped<ISpecificationService, SpecificationService>();
        services.AddScoped<IProductSpecificationService, ProductSpecificationService>();
        services.AddScoped<ISettingService, SettingService>();

        return services;
    }
}
