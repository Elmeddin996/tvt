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
        ICategoryRepository categoryRepository)
    {
        _context = context;
        Products = productRepository;
        Categories = categoryRepository;
    }

    public IProductRepository Products { get; }
    public ICategoryRepository Categories { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
