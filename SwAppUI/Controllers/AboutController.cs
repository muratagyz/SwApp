using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SwAppData.Enum;
using SwAppService.Services.Panel;
using SwAppViewModel.MultiUI;
using SwAppViewModel.UI;

namespace SwAppUI.Controllers;

public class AboutController : Controller
{
    private readonly IAboutOurWhoService aboutOurWhoService;
    private readonly IAboutService aboutService;
    private readonly IAboutSubTableService aboutSubTableService;
    private readonly IHomeWhySorswareService homeWhySorswareService;
    private readonly IMapper mapper;

    public AboutController(IAboutOurWhoService aboutOurWhoService, IHomeWhySorswareService homeWhySorswareService,
        IAboutService aboutService, IAboutSubTableService aboutSubTableService, IMapper mapper)
    {
        this.aboutOurWhoService = aboutOurWhoService;
        this.mapper = mapper;
        this.homeWhySorswareService = homeWhySorswareService;
        this.aboutSubTableService = aboutSubTableService;
        this.aboutService = aboutService;
    }

    public IActionResult Index()
    {
        var vm = new AboutMultiModel();

        var modelAboutOurWho = aboutOurWhoService.AboutOurGet();
        var dataModelAboutOurWho = mapper.Map<AboutOurWhoUI>(modelAboutOurWho);

        var modelHomeWhySorsware = homeWhySorswareService.HomeWhySorswareGetAll().Where(x => x.Status == Stat.Active);
        var dataModelHomeWhySorsware = mapper.Map<List<HomeWhySorswareUI>>(modelHomeWhySorsware);

        var modelAbout = aboutService.AboutGet();
        var dataModelAbout = mapper.Map<AboutUI>(modelAbout);

        var modelAboutSubTable = aboutSubTableService.AboutSubTableGetAll().Where(x => x.Status == Stat.Active);
        var dataModelAboutSubTable = mapper.Map<List<AboutSubTableUI>>(modelAboutSubTable);

        vm.AboutOurWhoUI = dataModelAboutOurWho;
        vm.HomeWhySorswareUI = dataModelHomeWhySorsware;
        vm.AboutUI = dataModelAbout;
        vm.AboutSubTableUI = dataModelAboutSubTable;

        return View(vm);
    }

    public IActionResult HomeWhySorswarePartial()
    {
        return PartialView();
    }

    public IActionResult SubTablePartial()
    {
        return PartialView();
    }
}