using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwAppService.Services.FileService;
using SwAppService.Services.Panel;
using SwAppViewModel.Panel;

namespace SwAppPanel.Controllers;

[Authorize]
public class ProjectController : Controller
{
    private readonly IFileImageService fileImageService;
    private readonly IProjectService projectService;
    private readonly IValidator<ProjectVM> validator;

    public ProjectController(IProjectService projectService, IValidator<ProjectVM> validator,
        IFileImageService fileImageService)
    {
        this.projectService = projectService;
        this.validator = validator;
        this.fileImageService = fileImageService;
    }

    public IActionResult Index()
    {
        var model = projectService.ProjectGetAll();
        return View(model);
    }

    public IActionResult ProjectCreate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ProjectCreate(ProjectVM data, IFormFile formFile)
    {
        var result = validator.Validate(data);
        if (result.IsValid)
        {
            data.ProjectImageUrl = fileImageService.GetImagePath(formFile);
            var model = projectService.ProjectAdd(data, HttpContext.Session.GetString("Username"));
            if (model)
                return RedirectToAction("Index");
            return RedirectToAction("ProjectCreate");
        }

        foreach (var error in result.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        return View(data);
    }

    public IActionResult ProjectDelete(int id)
    {
        var model = projectService.ProjectDelete(id, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }

    public IActionResult ProjectUpdate(int id)
    {
        var model = projectService.ProjectGetById(id);
        if (model != null)
            return View(model);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ProjectUpdate(ProjectVM data, IFormFile formFile)
    {
        var imageData = fileImageService.GetImagePath(formFile);
        data.ProjectImageUrl = imageData == null ? data.ProjectImageUrl : imageData;
        var model = projectService.ProjectUpdate(data, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }
}