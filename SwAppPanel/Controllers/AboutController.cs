using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwAppService.Services.Panel;
using SwAppViewModel.Panel;

namespace SwAppPanel.Controllers;

[Authorize]
public class AboutController : Controller
{
    private readonly IAboutService aboutService;

    public AboutController(IAboutService aboutService)
    {
        this.aboutService = aboutService;
    }

    public IActionResult Index()
    {
        var model = aboutService.AboutGet();
        if (model != null)
            return View(model);
        return View();
    }

    [HttpPost]
    public IActionResult Index(AboutVM data)
    {
        if (data.Id == 0)
            aboutService.AboutAdd(data, HttpContext.Session.GetString("Username"));
        else
            aboutService.AboutUpdate(data, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }
}