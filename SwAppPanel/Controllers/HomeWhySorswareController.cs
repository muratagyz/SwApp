using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwAppService.Services.FileService;
using SwAppService.Services.Panel;
using SwAppViewModel.Panel;

namespace SwAppPanel.Controllers;

[Authorize]
public class HomeWhySorswareController : Controller
{
    private readonly IFileImageService fileImageService;
    private readonly IHomeWhySorswareService homeWhySorswareService;
    private readonly IValidator<HomeWhySorswareVM> validator;

    public HomeWhySorswareController(IHomeWhySorswareService homeWhySorswareService,
        IValidator<HomeWhySorswareVM> validator, IFileImageService fileImageService)
    {
        this.homeWhySorswareService = homeWhySorswareService;
        this.validator = validator;
        this.fileImageService = fileImageService;
    }

    public IActionResult Index()
    {
        var model = homeWhySorswareService.HomeWhySorswareGetAll();
        return View(model);
    }

    public IActionResult HomeWhySorswareCreate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult HomeWhySorswareCreate(HomeWhySorswareVM data, IFormFile formFile)
    {
        var result = validator.Validate(data);
        if (result.IsValid)
        {
            data.HomeWhySorswareSubImageUrl = fileImageService.GetImagePath(formFile);
            var model = homeWhySorswareService.HomeWhySorswareAdd(data, HttpContext.Session.GetString("Username"));
            if (model)
                return RedirectToAction("Index");
            return RedirectToAction("HomeWhySorswareCreate");
        }

        foreach (var error in result.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        return View(data);
    }

    public IActionResult HomeWhySorswareDelete(int id)
    {
        var model = homeWhySorswareService.HomeWhySorswareDelete(id, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }

    public IActionResult HomeWhySorswareUpdate(int id)
    {
        var model = homeWhySorswareService.HomeWhySorswareGetById(id);
        if (model != null)
            return View(model);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult HomeWhySorswareUpdate(HomeWhySorswareVM data, IFormFile formFile)
    {
        var imageData = fileImageService.GetImagePath(formFile);
        data.HomeWhySorswareSubImageUrl = imageData == null ? data.HomeWhySorswareSubImageUrl : imageData;
        var model = homeWhySorswareService.HomeWhySorswareUpdate(data, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }
}