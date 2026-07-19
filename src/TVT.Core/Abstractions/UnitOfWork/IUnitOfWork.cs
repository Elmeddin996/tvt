using TVT.Core.Abstractions.Repositories;

namespace TVT.Core.Abstractions.UnitOfWork;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    IBrandRepository Brands { get; }
    INewsRepository News { get; }
    IPageRepository Pages { get; }
    ISliderRepository Sliders { get; }
    ISettingRepository Settings { get; }
    ISubscriberRepository Subscribers { get; }
    IContactMessageRepository ContactMessages { get; }

    Task<int> SaveChangesAsync();
}
