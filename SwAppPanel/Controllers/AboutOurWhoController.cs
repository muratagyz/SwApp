using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwAppService.Services.Panel;
using SwAppViewModel.Panel;

namespace SwAppPanel.Controllers;

[Authorize]
public class AboutOurWhoController : Controller
{
    private readonly IAboutOurWhoService aboutOurWhoService;

    public AboutOurWhoController(IAboutOurWhoService aboutOurWhoService)
    {
        this.aboutOurWhoService = aboutOurWhoService;
    }

    public IActionResult Index()
    {
        var model = aboutOurWhoService.AboutOurGet();
        if (model != null)
            return View(model);
        return View();
    }

    [HttpPost]
    public IActionResult Index(AboutOurWhoVM data)
    {
        if (data.Id == 0)
            aboutOurWhoService.AboutOurAdd(data, HttpContext.Session.GetString("Username"));

        else
            aboutOurWhoService.AboutOurUpdate(data, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }
}