using Microsoft.AspNetCore.Mvc;
using SwAppService.Services.Panel;
using SwAppViewModel.General;
using SwAppViewModel.Panel;

namespace SwAppAPI.Controllers;

[ApiController]
[Route("[action]")]
public class AdminAuthController : ControllerBase
{
    public IPanelLoginService panelLoginService;
    public IPanelRegisterService panelRegisterService;

    public AdminAuthController(IPanelRegisterService panelRegisterService, IPanelLoginService panelLoginService)
    {
        this.panelRegisterService = panelRegisterService;
        this.panelLoginService = panelLoginService;
    }

    [HttpPost(Name = "RegisterAdmin")]
    public PanelRegisterOpResult RegisterAdmin(PanelRegisterVM Data)
    {
        var result = panelRegisterService.PanelRegister(Data);
        return result;
    }

    [HttpPost(Name = "LoginAdmin")]
    public PanelLoginResult LoginAdmin(PanelLoginVM Data)
    {
        var result = panelLoginService.PanelLogin(Data);
        return result;
    }
}