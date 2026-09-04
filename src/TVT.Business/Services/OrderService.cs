using AutoMapper;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Orders;
using TVT.Core.Abstractions.UnitOfWork;
using TVT.Core.Common.Pagination;
using TVT.Core.Entities;

namespace TVT.Business.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<int> CreateAsync(CreateOrderDto dto)
    {
        if (dto.Items == null || !dto.Items.Any())
            throw new InvalidOperationException(
                "Order must contain at least one product.");


        var order = new Order
        {
            CustomerName = dto.CustomerName,
            Phone = dto.Phone,
            Email = dto.Email,
            Address = dto.Address,
            Status = "Pending"
        };


        decimal totalAmount = 0;


        foreach (var item in dto.Items)
        {
            if (item.Quantity <= 0)
                throw new InvalidOperationException(
                    "Invalid product quantity.");


            var product =
                await _unitOfWork.Products.GetByIdAsync(
                    item.ProductId);


            if (product is null)
                throw new KeyNotFoundException(
                    $"Product with ID {item.ProductId} not found.");


            if (product.StockQuantity < item.Quantity)
                throw new InvalidOperationException(
                    $"Not enough stock for product: {product.NameAz}");


            var totalPrice =
                product.Price * item.Quantity;


            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price,
                TotalPrice = totalPrice
            };


            order.OrderItems.Add(orderItem);


            product.StockQuantity -= item.Quantity;


            totalAmount += totalPrice;
        }


        order.TotalAmount = totalAmount;


        await _unitOfWork.Orders.AddAsync(order);

        await _unitOfWork.SaveChangesAsync();


        return order.Id;
    }


    public async Task<PagedResult<OrderListDto>> GetPagedAsync(
        PagedRequest request)
    {
        var result =
            await _unitOfWork.Orders.GetPagedAsync(request);


        return new PagedResult<OrderListDto>
        {
            Items = _mapper.Map<List<OrderListDto>>(
                result.Items),

            CurrentPage = result.CurrentPage,

            PageSize = result.PageSize,

            TotalCount = result.TotalCount
        };
    }


    public async Task<OrderDetailDto?> GetByIdAsync(int id)
    {
        var order =
            await _unitOfWork.Orders.GetByIdWithItemsAsync(id);


        if (order is null)
            return null;


        return _mapper.Map<OrderDetailDto>(order);
    }


    public async Task UpdateStatusAsync(
        int id,
        string status)
    {
        var order =
            await _unitOfWork.Orders.GetByIdAsync(id);


        if (order is null)
            throw new KeyNotFoundException(
                "Order not found.");


        order.Status = status;

        order.UpdatedDate = DateTime.UtcNow;


        await _unitOfWork.SaveChangesAsync();
    }
}
