using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwAppService.Services.FileService;
using SwAppService.Services.Panel;
using SwAppViewModel.Panel;

namespace SwAppPanel.Controllers;

[Authorize]
public class HomeSliderController : Controller
{
    private readonly IFileImageService fileImageService;
    private readonly IHomeSliderService homeSliderService;
    private readonly IValidator<HomeSliderVM> validator;

    public HomeSliderController(IHomeSliderService homeSliderService, IValidator<HomeSliderVM> validator,
        IFileImageService fileImageService)
    {
        this.homeSliderService = homeSliderService;
        this.validator = validator;
        this.fileImageService = fileImageService;
    }

    public IActionResult Index()
    {
        var model = homeSliderService.HomeSliderGetAll();
        return View(model);
    }

    public IActionResult HomeSliderCreate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult HomeSliderCreate(HomeSliderVM data, IFormFile formFile)
    {
        var result = validator.Validate(data);
        if (result.IsValid)
        {
            data.HomeSliderImageUrl = fileImageService.GetImagePath(formFile);
            var model = homeSliderService.HomeSliderAdd(data, HttpContext.Session.GetString("Username"));
            if (model)
                return RedirectToAction("Index");
            return RedirectToAction("HomeSliderCreate");
        }

        foreach (var error in result.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        return View(data);
    }

    public IActionResult HomeSliderDelete(int id)
    {
        var model = homeSliderService.HomeSliderDelete(id, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }

    public IActionResult HomeSliderUpdate(int id)
    {
        var model = homeSliderService.HomeSliderGetById(id);
        if (model != null)
            return View(model);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult HomeSliderUpdate(HomeSliderVM data, IFormFile formFile)
    {
        var imageData = fileImageService.GetImagePath(formFile);
        data.HomeSliderImageUrl = imageData == null ? data.HomeSliderImageUrl : imageData;
        var model = homeSliderService.HomeSliderUpdate(data, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }
}