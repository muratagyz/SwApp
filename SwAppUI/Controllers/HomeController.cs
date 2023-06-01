using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SwAppData.Enum;
using SwAppService.Services.Panel;
using SwAppUI.Models;
using SwAppViewModel.MultiUI;
using SwAppViewModel.UI;

namespace SwAppUI.Controllers;

public class HomeController : Controller
{
    private readonly IHomeProjectWorkService homeProjectWorkService;
    private readonly IHomeSliderService homeSliderService;
    private readonly IHomeWhatAreWeDoingService homeWhatAreWeDoingService;
    private readonly IHomeWhySorswareService homeWhySorswareService;
    private readonly IMapper mapper;
    private readonly IProjectService projectService;

    public HomeController(IHomeSliderService homeSliderService, IMapper mapper,
        IHomeWhySorswareService homeWhySorswareService, IHomeProjectWorkService homeProjectWorkService,
        IProjectService projectService, IHomeWhatAreWeDoingService homeWhatAreWeDoingService)
    {
        this.homeSliderService = homeSliderService;
        this.mapper = mapper;
        this.homeWhySorswareService = homeWhySorswareService;
        this.homeProjectWorkService = homeProjectWorkService;
        this.projectService = projectService;
        this.homeWhatAreWeDoingService = homeWhatAreWeDoingService;
    }

    public IActionResult Index()
    {
        var hm = new HomeMultiModel();

        var modelHomeSlider = homeSliderService.HomeSliderGetAll().Where(x => x.Status == Stat.Active);
        var dataHomeSlider = mapper.Map<List<HomeSliderUI>>(modelHomeSlider);

        var modelHomeWhySorsware = homeWhySorswareService.HomeWhySorswareGetAll().Where(x => x.Status == Stat.Active);
        var dataModelHomeWhySorsware = mapper.Map<List<HomeWhySorswareUI>>(modelHomeWhySorsware);

        var modelHomeProjectWork = homeProjectWorkService.HomeProjectWorkGetAll().Where(x => x.Status == Stat.Active);
        var dataModelHomeProjectWor = mapper.Map<List<HomeProjectWorkUI>>(modelHomeProjectWork);

        var modelProject = projectService.ProjectGetAll().Where(x => x.Status == Stat.Active);
        var dataModelProject = mapper.Map<List<ProjectUI>>(modelProject);

        var modelHomeWADS = homeWhatAreWeDoingService.HomeWhatAreWeGet();
        var dataModelHomeWADS = mapper.Map<HomeWhatAreWeDoingUI>(modelHomeWADS);

        hm.HomeSliderUI = dataHomeSlider;
        hm.HomeWhySorswareUI = dataModelHomeWhySorsware;
        hm.HomeProjectWorkUI = dataModelHomeProjectWor;
        hm.ProjectUI = dataModelProject;
        hm.HomeWhatAreWeDoingUI = dataModelHomeWADS;

        return View(hm);
    }

    public IActionResult HomeSubTableServicePartial()
    {
        return PartialView();
    }

    [HttpPost]
    public IActionResult SendMessage(HomeMultiModel message)
    {
        try
        {
            var fromAddress = new MailAddress(message.ContactMessage.Email);
            var toAddress = new MailAddress("");
            const string subject = "Sorsware İletişim Maili Maili";
            using (var smtp = new SmtpClient
                   {
                       Host = "smtp.gmail.com",
                       Port = 587,
                       EnableSsl = true,
                       DeliveryMethod = SmtpDeliveryMethod.Network,
                       UseDefaultCredentials = false,
                       Credentials = new NetworkCredential("", "")
                   })
            {
                using (var _message = new MailMessage(fromAddress, toAddress)
                       {
                           Subject = subject,
                           Body = "Gönderen Ad Soyad: " + message.ContactMessage.Fullname + " - Gönderen Mesaj: " +
                                  message.ContactMessage.Message + " - Gönderen Mail: " + message.ContactMessage.Email +
                                  " - Gönderen Telefon Numarası: " + message.ContactMessage.Number
                       })
                {
                    smtp.Send(_message);
                }
            }

            ViewBag.Message = "teşekkürler Mailiniz başarı bir şekilde gönderildi";

            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            ViewBag.Message = "Mesaj Gönderilken hata olıuştu";
            return RedirectToAction("Index");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}