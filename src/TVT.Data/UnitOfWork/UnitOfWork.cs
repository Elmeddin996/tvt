using TVT.Core.Abstractions.Repositories;
using TVT.Core.Abstractions.UnitOfWork;
using TVT.Data.Context;

namespace TVT.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(
        ApplicationDbContext context,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IBrandRepository brandRepository,
        INewsRepository newsRepository,
        IPageRepository pageRepository,
        ISliderRepository sliderRepository,
        IMiniSliderRepository miniSliderRepository,
        IMobileSliderRepository mobileSliderRepository,
        ISettingRepository settingRepository,
        ISubscriberRepository subscriberRepository,
        IContactMessageRepository contactMessageRepository,
        ISpecificationGroupRepository specificationGroupRepository,
        ISpecificationRepository specificationRepository,
        IProductImageRepository productImageRepository,
        IProductSpecificationRepository productSpecificationRepository)
    {
        _context = context;

        Products = productRepository;
        ProductImages = productImageRepository;
        ProductSpecifications = productSpecificationRepository;

        Categories = categoryRepository;
        Brands = brandRepository;

        SpecificationGroups = specificationGroupRepository;
        Specifications = specificationRepository;

        News = newsRepository;
        Pages = pageRepository;

        Sliders = sliderRepository;
        MiniSliders = miniSliderRepository;
        MobileSliders = mobileSliderRepository;

        Settings = settingRepository;
        Subscribers = subscriberRepository;
        ContactMessages = contactMessageRepository;
    }

    public IProductRepository Products { get; }

    public IProductImageRepository ProductImages { get; }

    public IProductSpecificationRepository ProductSpecifications { get; }

    public ISpecificationGroupRepository SpecificationGroups { get; }

    public ISpecificationRepository Specifications { get; }

    public ICategoryRepository Categories { get; }

    public IBrandRepository Brands { get; }

    public INewsRepository News { get; }

    public IPageRepository Pages { get; }

    public ISliderRepository Sliders { get; }

    public IMiniSliderRepository MiniSliders { get; }

    public IMobileSliderRepository MobileSliders { get; }

    public ISettingRepository Settings { get; }

    public ISubscriberRepository Subscribers { get; }

    public IContactMessageRepository ContactMessages { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
