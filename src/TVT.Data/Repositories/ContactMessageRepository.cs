using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class ContactMessageRepository : GenericRepository<ContactMessage>, IContactMessageRepository
{
    public ContactMessageRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<List<ContactMessage>> GetUnreadMessagesAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Where(m => !m.IsRead)
            .OrderByDescending(m => m.CreatedDate)
            .ToListAsync();
    }
}
