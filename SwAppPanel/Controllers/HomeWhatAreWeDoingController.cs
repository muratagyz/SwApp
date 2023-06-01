using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwAppService.Services.Panel;
using SwAppViewModel.Panel;

namespace SwAppPanel.Controllers;

[Authorize]
public class HomeWhatAreWeDoingController : Controller
{
    private readonly IHomeWhatAreWeDoingService homeWhatAreWeDoingService;

    public HomeWhatAreWeDoingController(IHomeWhatAreWeDoingService homeWhatAreWeDoingService)
    {
        this.homeWhatAreWeDoingService = homeWhatAreWeDoingService;
    }

    public IActionResult Index()
    {
        var model = homeWhatAreWeDoingService.HomeWhatAreWeGet();
        if (model != null)
            return View(model);
        return View();
    }

    [HttpPost]
    public IActionResult Index(HomeWhatAreWeDoingVM data)
    {
        if (data.Id == 0)
            homeWhatAreWeDoingService.HomeWhatAreWeAdd(data, HttpContext.Session.GetString("Username"));

        else
            homeWhatAreWeDoingService.HomeWhatAreWeUpdate(data, HttpContext.Session.GetString("Username"));

        return RedirectToAction("Index");
    }
}