using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwAppService.Services.Panel;
using SwAppViewModel.Panel;

namespace SwAppPanel.Controllers;

[Authorize]
public class ContactInfoController : Controller
{
    private readonly IContactInfoService contactInfoService;
    private readonly IValidator<ContactInfoVM> validator;

    public ContactInfoController(IContactInfoService contactInfoService, IValidator<ContactInfoVM> validator)
    {
        this.contactInfoService = contactInfoService;
        this.validator = validator;
    }

    public IActionResult Index()
    {
        var model = contactInfoService.ContactInfoWorkGetAll();
        return View(model);
    }

    public IActionResult ContactInfoCreate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ContactInfoCreate(ContactInfoVM data)
    {
        var result = validator.Validate(data);
        if (result.IsValid)
        {
            var model = contactInfoService.ContactInfoAdd(data, HttpContext.Session.GetString("Username"));
            if (model)
                return RedirectToAction("Index");
            return RedirectToAction("ContactInfoCreate");
        }

        foreach (var error in result.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        return View(data);
    }

    public IActionResult ContactInfoDelete(int id)
    {
        var model = contactInfoService.ContactInfoDelete(id, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }

    public IActionResult ContactInfoUpdate(int id)
    {
        var model = contactInfoService.ContactInfoGetById(id);
        if (model != null)
            return View(model);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ContactInfoUpdate(ContactInfoVM data)
    {
        var model = contactInfoService.ContactInfoUpdate(data, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }
}