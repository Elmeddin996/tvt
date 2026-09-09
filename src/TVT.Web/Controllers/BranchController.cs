using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;

namespace TVT.Web.Controllers;

public class BranchController : Controller
{
    private readonly IBranchService _branchService;

    public BranchController(IBranchService branchService)
    {
        _branchService = branchService;
    }

    public async Task<IActionResult> Index()
    {
        var branches = await _branchService.GetAllActiveAsync();

        return View(branches);
    }
}
