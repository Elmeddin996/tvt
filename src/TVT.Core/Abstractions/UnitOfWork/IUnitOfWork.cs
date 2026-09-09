using TVT.Core.Abstractions.Repositories;

namespace TVT.Core.Abstractions.UnitOfWork;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    IOrderRepository Orders { get; }
    ICategoryRepository Categories { get; }
    IBrandRepository Brands { get; }
    IBranchRepository Branches { get; }
    INewsRepository News { get; }
    IPageRepository Pages { get; }
    ISliderRepository Sliders { get; }
    IMiniSliderRepository MiniSliders { get; }
    IMobileSliderRepository MobileSliders { get; }
    ISettingRepository Settings { get; }
    ISubscriberRepository Subscribers { get; }
    IContactMessageRepository ContactMessages { get; }

    IProductImageRepository ProductImages { get; }
    ISpecificationGroupRepository SpecificationGroups { get; }
    ISpecificationRepository Specifications { get; }
    IProductSpecificationRepository ProductSpecifications { get; }

    Task<int> SaveChangesAsync();
}
