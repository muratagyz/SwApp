using AutoMapper;
using SwAppData.Entity;
using SwAppViewModel.Panel;
using SwAppViewModel.UI;

namespace SwAppService.Mapping;

public class MapProfile : Profile
{
    public MapProfile()
    {
        //Panel

        CreateMap<User, PanelRegisterVM>().ReverseMap();
        CreateMap<User, PanelLoginVM>().ReverseMap();
        CreateMap<Log, LogVM>().ReverseMap();
        CreateMap<HomeSlider, HomeSliderVM>().ReverseMap();
        CreateMap<About, AboutVM>().ReverseMap();
        CreateMap<AboutOurWho, AboutOurWhoVM>().ReverseMap();
        CreateMap<AboutSubTable, AboutSubTableVM>().ReverseMap();
        CreateMap<ContactInfo, ContactInfoVM>().ReverseMap();
        CreateMap<Personal, PersonalVM>().ReverseMap();
        CreateMap<Project, ProjectVM>().ReverseMap();
        CreateMap<Service, ServiceVM>().ReverseMap();
        CreateMap<HomeProjectWork, HomeProjectWorkVM>().ReverseMap();
        CreateMap<HomeWhySorsware, HomeWhySorswareVM>().ReverseMap();
        CreateMap<User, UserVM>().ReverseMap();
        CreateMap<ContactInformation, ContactInformationVM>().ReverseMap();
        CreateMap<HomeWhatAreWeDoing, HomeWhatAreWeDoingVM>().ReverseMap();

        //UI

        CreateMap<AboutOurWhoVM, AboutOurWhoUI>().ReverseMap();
        CreateMap<AboutSubTableVM, AboutSubTableUI>().ReverseMap();
        CreateMap<AboutVM, AboutUI>().ReverseMap();
        CreateMap<ContactInfoVM, ContactInfoUI>().ReverseMap();
        CreateMap<HomeProjectWorkVM, HomeProjectWorkUI>().ReverseMap();
        CreateMap<HomeSliderVM, HomeSliderUI>().ReverseMap();
        CreateMap<HomeWhySorswareVM, HomeWhySorswareUI>().ReverseMap();
        CreateMap<PersonalVM, PersonalUI>().ReverseMap();
        CreateMap<ProjectVM, ProjectUI>().ReverseMap();
        CreateMap<ServiceVM, ServiceUI>().ReverseMap();
        CreateMap<HomeWhatAreWeDoingVM, HomeWhatAreWeDoingUI>().ReverseMap();
        CreateMap<ContactInformationVM, ContactInformationUI>().ReverseMap();
    }
}