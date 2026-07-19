using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface ISubscriberRepository : IGenericRepository<Subscriber>
{
    Task<Subscriber?> GetByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
}
