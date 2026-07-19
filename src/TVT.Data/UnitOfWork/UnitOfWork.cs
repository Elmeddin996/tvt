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
        ISettingRepository settingRepository,
        ISubscriberRepository subscriberRepository,
        IContactMessageRepository contactMessageRepository)
    {
        _context = context;
        Products = productRepository;
        Categories = categoryRepository;
        Brands = brandRepository;
        News = newsRepository;
        Pages = pageRepository;
        Sliders = sliderRepository;
        Settings = settingRepository;
        Subscribers = subscriberRepository;
        ContactMessages = contactMessageRepository;
    }

    public IProductRepository Products { get; }
    public ICategoryRepository Categories { get; }
    public IBrandRepository Brands { get; }
    public INewsRepository News { get; }
    public IPageRepository Pages { get; }
    public ISliderRepository Sliders { get; }
    public ISettingRepository Settings { get; }
    public ISubscriberRepository Subscribers { get; }
    public IContactMessageRepository ContactMessages { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
