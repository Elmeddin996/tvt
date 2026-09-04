using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;
using TVT.Core.Common.Pagination;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class OrderController : BaseAdminController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> Index(PagedRequest request)
    {
        request.PageSize = 20;

        ViewData["Title"] = "Orders";
        ViewBag.Search = request.Search;

        var orders =
            await _orderService.GetPagedAsync(request);

        return View(orders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var order =
            await _orderService.GetByIdAsync(id);

        if (order is null)
            return NotFound();

        ViewData["Title"] = $"Order #{order.Id}";

        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(
        int id,
        string status)
    {
        var allowedStatuses = new[]
        {
            "Pending",
            "Confirmed",
            "Preparing",
            "Shipped",
            "Delivered",
            "Cancelled"
        };

        if (!allowedStatuses.Contains(status))
        {
            TempData["Error"] = "Invalid order status.";

            return RedirectToAction(
                nameof(Details),
                new { id });
        }

        try
        {
            await _orderService.UpdateStatusAsync(id, status);

            TempData["Success"] =
                $"Order #{id} status updated successfully.";
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            TempData["Error"] =
                "An error occurred while updating the order status.";
        }

        return RedirectToAction(
            nameof(Details),
            new { id });
    }
}
