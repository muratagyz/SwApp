using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwAppService.Services.Panel;
using SwAppViewModel.Panel;

namespace SwAppPanel.Controllers;

[Authorize]
public class ContactInformationController : Controller
{
    private readonly IContactInformationService contactInformationService;
    private readonly IValidator<ContactInformationVM> validator;

    public ContactInformationController(IContactInformationService contactInformationService,
        IValidator<ContactInformationVM> validator)
    {
        this.contactInformationService = contactInformationService;
        this.validator = validator;
    }

    public IActionResult Index()
    {
        var model = contactInformationService.ContactInformationGetAll();
        return View(model);
    }

    public IActionResult ContactInformationCreate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ContactInformationCreate(ContactInformationVM data)
    {
        var result = validator.Validate(data);
        if (result.IsValid)
        {
            var model = contactInformationService.ContactInformationAdd(data,
                HttpContext.Session.GetString("Username"));
            if (model)
                return RedirectToAction("Index");
            return RedirectToAction("ContactInformationCreate");
        }

        foreach (var error in result.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        return View(data);
    }

    public IActionResult ContactInformationDelete(int id)
    {
        var model = contactInformationService.ContactInformationDelete(id, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }

    public IActionResult ContactInformationUpdate(int id)
    {
        var model = contactInformationService.ContactInformationGetById(id);
        if (model != null)
            return View(model);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ContactInformationUpdate(ContactInformationVM data)
    {
        var model = contactInformationService.ContactInformationUpdate(data, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }
}