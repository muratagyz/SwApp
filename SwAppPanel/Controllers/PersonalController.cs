using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwAppService.Services.FileService;
using SwAppService.Services.Panel;
using SwAppViewModel.Panel;

namespace SwAppPanel.Controllers;

[Authorize]
public class PersonalController : Controller
{
    private readonly IFileImageService fileImageService;
    private readonly IPersonalService personalService;
    private readonly IValidator<PersonalVM> validator;

    public PersonalController(IPersonalService personalService, IValidator<PersonalVM> validator,
        IFileImageService fileImageService)
    {
        this.personalService = personalService;
        this.validator = validator;
        this.fileImageService = fileImageService;
    }

    public IActionResult Index()
    {
        var model = personalService.PersonalGetAll();
        return View(model);
    }

    public IActionResult PersonalCreate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult PersonalCreate(PersonalVM data, IFormFile formFile)
    {
        var result = validator.Validate(data);
        if (result.IsValid)
        {
            data.PersonalImageUrl = fileImageService.GetImagePath(formFile);
            var model = personalService.PersonalAdd(data, HttpContext.Session.GetString("Username"));
            if (model)
                return RedirectToAction("Index");
            return RedirectToAction("PersonalCreate");
        }

        foreach (var error in result.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        return View(data);
    }

    public IActionResult PersonalDelete(int id)
    {
        var model = personalService.PersonalDelete(id, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }

    public IActionResult PersonalUpdate(int id)
    {
        var model = personalService.PersonalGetById(id);
        if (model != null)
            return View(model);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult PersonalUpdate(PersonalVM data, IFormFile formFile)
    {
        var imageData = fileImageService.GetImagePath(formFile);
        data.PersonalImageUrl = imageData == null ? data.PersonalImageUrl : imageData;
        var model = personalService.PersonalUpdate(data, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }
}