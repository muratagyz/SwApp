using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SwAppPanel.Controllers;

[Authorize]
public class PanelController : Controller
{
    public IActionResult Index()
    {
        TempData["FullName"] = HttpContext.Session.GetString("Fullname");
        return View();
    }
}