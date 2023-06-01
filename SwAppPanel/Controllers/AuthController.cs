using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwAppData.Messages;
using SwAppService.Services.General;
using SwAppService.Services.Panel;
using SwAppViewModel.General;
using SwAppViewModel.Panel;

namespace SwAppPanel.Controllers;

public class AuthController : Controller
{
    private readonly IUtilsService utilsService;
    private readonly IMapper mapper;
    public IPanelLoginService panelLoginService;
    public IPanelRegisterService panelRegisterService;
    private readonly IValidator<PanelLoginVM> validatorLogin;
    private readonly IValidator<PanelRegisterVM> validatorRegister;

    public AuthController(IPanelRegisterService panelRegisterService, IPanelLoginService panelLoginService,
        IUtilsService utilsService, IValidator<PanelLoginVM> validatorLogin,
        IValidator<PanelRegisterVM> validatorRegister, IMapper mapper)
    {
        this.panelRegisterService = panelRegisterService;
        this.panelLoginService = panelLoginService;
        this.utilsService = utilsService;
        this.validatorLogin = validatorLogin;
        this.validatorRegister = validatorRegister;
        this.mapper = mapper;
    }

    public IActionResult Login(string? Control)
    {
        ViewBag.control = Control;
        return View();
    }

    [HttpPost]
    public IActionResult Login(PanelLoginVM Data)
    {
        var resultLogin = validatorLogin.Validate(Data);

        if (resultLogin.IsValid)
        {
            var result = panelLoginService.PanelLogin(Data);

            if (result.IsSuccess)
            {
                var user = mapper.Map<PanelLoginOpSuccessResult>(result);
                HttpContext.Session.SetString("JWToken", user.Token);
                HttpContext.Session.SetString("Username", user.UserName);
                HttpContext.Session.SetString("UserRole", user.Role.ToString());
                HttpContext.Session.SetString("Fullname", panelLoginService.PanelLoginGetFullName(Data.UserName));
                return RedirectToAction("Index", "Panel");
            }

            ViewBag.UserLoginError = Message.UserLoginError;
        }
        else
        {
            foreach (var error in resultLogin.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(PanelRegisterVM Data)
    {
        var resultRegister = validatorRegister.Validate(Data);
        if (resultRegister.IsValid)
        {
            var result = panelRegisterService.PanelRegister(Data);

            if (result.OpDescription == Message.UserNameControl)
            {
                ViewBag.UserNameChangeError = Message.UserNameControl;
                return View();
            }

            if (result.IsSuccess)
                return RedirectToAction("Login", new { Control = Message.UserStatusControl });
        }
        else
        {
            foreach (var error in resultRegister.Errors)
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return View();
    }

    [Authorize]
    public IActionResult LogOut()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}