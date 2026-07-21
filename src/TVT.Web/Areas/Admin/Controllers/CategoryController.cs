using Microsoft.AspNetCore.Mvc;
using TVT.Business.DTOs.Categories;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Categories";
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(CreateCategoryDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        return RedirectToAction(nameof(Index));
    }
}