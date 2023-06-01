using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwAppService.Services.FileService;
using SwAppService.Services.Panel;
using SwAppViewModel.Panel;

namespace SwAppPanel.Controllers;

[Authorize]
public class ServiceController : Controller
{
    private readonly IFileImageService fileImageService;
    private readonly IServicesService servicesService;
    private readonly IValidator<ServiceVM> validator;

    public ServiceController(IServicesService servicesService, IValidator<ServiceVM> validator,
        IFileImageService fileImageService)
    {
        this.servicesService = servicesService;
        this.validator = validator;
        this.fileImageService = fileImageService;
    }

    public IActionResult Index()
    {
        var model = servicesService.ServiceGetAll();
        return View(model);
    }

    public IActionResult ServiceCreate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ServiceCreate(ServiceVM data, IFormFile formFile)
    {
        var result = validator.Validate(data);
        if (result.IsValid)
        {
            data.ServiceImageUrl = fileImageService.GetImagePath(formFile);
            var model = servicesService.ServiceAdd(data, HttpContext.Session.GetString("Username"));
            if (model)
                return RedirectToAction("Index");
            return RedirectToAction("ServiceCreate");
        }

        foreach (var error in result.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        return View(data);
    }

    public IActionResult ServiceDelete(int id)
    {
        var model = servicesService.ServiceDelete(id, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }

    public IActionResult ServiceUpdate(int id)
    {
        var model = servicesService.ServiceGetById(id);
        if (model != null)
            return View(model);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ServiceUpdate(ServiceVM data, IFormFile formFile)
    {
        var imageData = fileImageService.GetImagePath(formFile);
        data.ServiceImageUrl = imageData == null ? data.ServiceImageUrl : imageData;
        var model = servicesService.ServiceUpdate(data, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }
}