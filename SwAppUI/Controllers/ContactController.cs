using System.Net;
using System.Net.Mail;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SwAppData.Enum;
using SwAppService.Services.Panel;
using SwAppViewModel.MultiUI;
using SwAppViewModel.UI;

namespace SwAppUI.Controllers;

public class ContactController : Controller
{
    private readonly IContactInformationService contactInformationService;
    private readonly IContactInfoService contactInfoService;
    private readonly IMapper mapper;

    public ContactController(IContactInfoService contactInfoService,
        IContactInformationService contactInformationService, IMapper mapper)
    {
        this.contactInfoService = contactInfoService;
        this.contactInformationService = contactInformationService;
        this.mapper = mapper;
    }

    public IActionResult Index()
    {
        var cm = new ContactMultiModel();

        var ContactInfomodel = contactInfoService.ContactInfoWorkGetAll().Where(x => x.Status == Stat.Active);
        var ContactInfodataModel = mapper.Map<List<ContactInfoUI>>(ContactInfomodel);

        var ContactInfomationModel =
            contactInformationService.ContactInformationGetAll().Where(x => x.Status == Stat.Active);
        var ContactInformationDataModel = mapper.Map<List<ContactInformationUI>>(ContactInfomationModel);

        cm.ContactInfoUI = ContactInfodataModel;
        cm.ContactInformationUI = ContactInformationDataModel;

        return View(cm);
    }

    [HttpPost]
    public IActionResult SendMessage(HomeMultiModel message)
    {
        try
        {
            var fromAddress = new MailAddress(message.ContactMessage.Email);
            var toAddress = new MailAddress("");
            const string subject = "Sorsware Teklif ve  Detaylar Maili";
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
}