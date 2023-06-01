using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwAppService.Services.Panel;

namespace SwAppPanel.Controllers;

[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    public IActionResult Index()
    {
        var model = userService.UserGetAll();
        return View(model);
    }

    public IActionResult UserDelete(int id)
    {
        var model = userService.UserDelete(id, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }

    public IActionResult UserRoleUpdate(int id)
    {
        var model = userService.UserRoleUpdate(id, HttpContext.Session.GetString("Username"));
        return RedirectToAction("Index");
    }
}