using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface ISliderRepository : IGenericRepository<Slider>
{
    Task<List<Slider>> GetActiveSlidersAsync();
}
