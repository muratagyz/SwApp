using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwAppService.Services.FileService;
using SwAppService.Services.Panel;
using SwAppViewModel.Panel;

namespace SwAppPanel.Controllers;

[Authorize]
public class HomeProjectWorkController : Controller
{
    private readonly IFileImageService fileImageService;
    private readonly IHomeProjectWorkService homeProjectWorkService;
    private readonly IValidator<HomeProjectWorkVM> validator;

    public HomeProjectWorkController(IHomeProjectWorkService homeProjectWorkService,
        IValidator<HomeProjectWorkVM> validator, IFileImageService fileImageService)
    {
        this.homeProjectWorkService = homeProjectWorkService;
        this.validator = validator;
        this.fileImageService = fileImageService;
    }

    public IActionResult Index()
    {
        var model = homeProjectWorkService.HomeProjectWorkGetAll();
        return View(model);
    }

    public IActionResult HomeProjectWorkCreate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult HomeProjectWorkCreate(HomeProjectWorkVM data, IFormFile formFile)
    {
        var result = validator.Validate(data);
        if (result.IsValid)
        {
            data.HomeProjectWorkImageUrl = fileImageService.GetImagePath(formFile);
            var model = homeProjectWorkService.HomeProjectWorkAdd(data, HttpContext.Session.GetString("Username"));
            if (model)
                return RedirectToAction("Index");
            return RedirectToAction("HomeProjectWorkCreate");
        }

        foreach (var error in result.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        return View(data);
    }

    public IActionResult HomeProjectWorkDelete(int id)
    {
        var model = homeProjectWorkService.HomeProjectWorkDelete(id, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }

    public IActionResult HomeProjectWorkUpdate(int id)
    {
        var model = homeProjectWorkService.HomeProjectWorkGetById(id);
        if (model != null)
            return View(model);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult HomeProjectWorkUpdate(HomeProjectWorkVM data, IFormFile formFile)
    {
        var imageData = fileImageService.GetImagePath(formFile);
        data.HomeProjectWorkImageUrl = imageData == null ? data.HomeProjectWorkImageUrl : imageData;
        var model = homeProjectWorkService.HomeProjectWorkUpdate(data, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }
}