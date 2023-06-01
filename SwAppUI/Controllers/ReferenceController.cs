using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SwAppData.Enum;
using SwAppService.Services.Panel;
using SwAppViewModel.UI;

namespace SwAppUI.Controllers;

public class ReferenceController : Controller
{
    private readonly IMapper mapper;
    private readonly IProjectService projectService;

    public ReferenceController(IProjectService projectService, IMapper mapper)
    {
        this.projectService = projectService;
        this.mapper = mapper;
    }

    public IActionResult Index()
    {
        var model = projectService.ProjectGetAll().Where(x => x.Status == Stat.Active);
        var dataModel = mapper.Map<List<ProjectUI>>(model);
        return View(dataModel);
    }

    public PartialViewResult ProjectPartialView()
    {
        return PartialView();
    }
}