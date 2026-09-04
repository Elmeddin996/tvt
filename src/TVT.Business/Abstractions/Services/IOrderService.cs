using TVT.Business.DTOs.Orders;
using TVT.Core.Common.Pagination;

namespace TVT.Business.Abstractions.Services;

public interface IOrderService
{
    Task<int> CreateAsync(CreateOrderDto dto);

    Task<PagedResult<OrderListDto>> GetPagedAsync(
        PagedRequest request);

    Task<OrderDetailDto?> GetByIdAsync(int id);

    Task UpdateStatusAsync(
        int id,
        string status);
}
