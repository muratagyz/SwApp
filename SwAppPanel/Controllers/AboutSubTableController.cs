using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwAppService.Services.FileService;
using SwAppService.Services.Panel;
using SwAppViewModel.Panel;

namespace SwAppPanel.Controllers;

[Authorize]
public class AboutSubTableController : Controller
{
    private readonly IAboutSubTableService aboutSubTableService;
    private readonly IFileImageService fileImageService;
    private readonly IValidator<AboutSubTableVM> validator;

    public AboutSubTableController(IAboutSubTableService aboutSubTableService, IValidator<AboutSubTableVM> validator,
        IFileImageService fileImageService)
    {
        this.aboutSubTableService = aboutSubTableService;
        this.validator = validator;
        this.fileImageService = fileImageService;
    }

    public IActionResult Index()
    {
        var model = aboutSubTableService.AboutSubTableGetAll();
        return View(model);
    }

    public IActionResult AboutSubTableCreate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AboutSubTableCreate(AboutSubTableVM data, IFormFile formFile)
    {
        var result = validator.Validate(data);
        if (result.IsValid)
        {
            data.AboutSubTableImageUrl = fileImageService.GetImagePath(formFile);
            var model = aboutSubTableService.AboutSubTableAdd(data, HttpContext.Session.GetString("Username"));
            if (model)
                return RedirectToAction("Index");
            return RedirectToAction("AboutSubTableCreate");
        }

        foreach (var error in result.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        return View(data);
    }

    public IActionResult AboutSubTableDelete(int id)
    {
        var model = aboutSubTableService.AboutSubTableDelete(id, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }

    public IActionResult AboutSubTableUpdate(int id)
    {
        var model = aboutSubTableService.AboutSubTableGetById(id);
        if (model != null)
            return View(model);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult AboutSubTableUpdate(AboutSubTableVM data, IFormFile formFile)
    {
        var imageData = fileImageService.GetImagePath(formFile);
        data.AboutSubTableImageUrl = imageData == null ? data.AboutSubTableImageUrl : imageData;
        var model = aboutSubTableService.AboutSubTableUpdate(data, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }
}