using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SwAppData.Enum;
using SwAppService.Services.Panel;
using SwAppViewModel.UI;

namespace SwAppUI.Controllers;

public class PrivacyController : Controller
{
    private readonly IMapper mapper;
    private readonly IServicesService servicesService;

    public PrivacyController(IServicesService servicesService, IMapper mapper)
    {
        this.servicesService = servicesService;
        this.mapper = mapper;
    }

    public IActionResult Index()
    {
        var model = servicesService.ServiceGetAll().Where(x => x.Status == Stat.Active);
        var dataModel = mapper.Map<List<ServiceUI>>(model);
        return View(dataModel);
    }

    public PartialViewResult ServicePartialView()
    {
        return PartialView();
    }
}