using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Common.Pagination;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class BranchRepository : GenericRepository<Branch>, IBranchRepository
{
    public BranchRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<List<Branch>> GetAllActiveAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Where(x => x.IsActive && !x.IsDeleted)
            .OrderBy(x => x.DisplayOrder)
            .ThenBy(x => x.NameAz)
            .ToListAsync();
    }

    public async Task<PagedResult<Branch>> GetPagedAsync(PagedRequest request)
    {
        var query = DbSet
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim().ToLower();

            query = query.Where(x =>
                x.NameAz.ToLower().Contains(search) ||
                x.NameEn.ToLower().Contains(search) ||
                x.NameRu.ToLower().Contains(search) ||
                x.AddressAz.ToLower().Contains(search) ||
                x.AddressEn.ToLower().Contains(search) ||
                x.AddressRu.ToLower().Contains(search) ||
                x.Phone.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.DisplayOrder)
            .ThenBy(x => x.NameAz)
            .Skip((request.Page - 1) * 20)
            .Take(20)
            .ToListAsync();

        return new PagedResult<Branch>
        {
            Items = items,
            CurrentPage = request.Page,
            PageSize = 20,
            TotalCount = totalCount
        };
    }

    public async Task<Branch?> GetByIdAsync(int id)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
}
