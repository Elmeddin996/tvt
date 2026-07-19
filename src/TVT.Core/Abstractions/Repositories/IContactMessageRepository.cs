using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface IContactMessageRepository : IGenericRepository<ContactMessage>
{
    Task<List<ContactMessage>> GetUnreadMessagesAsync();
}
