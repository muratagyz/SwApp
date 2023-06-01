using Autofac;
using FluentValidation;
using SwAppService.Services.FileService;
using SwAppService.Services.General;
using SwAppService.Services.Panel;
using SwAppService.Validation;
using SwAppViewModel.Panel;

namespace SwAppService.AutoFac;

public class AutoFacServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<PanelRegisterService>().As<IPanelRegisterService>();
        builder.RegisterType<PanelRegisterValidatior>().As<IValidator<PanelRegisterVM>>();

        builder.RegisterType<PanelLoginService>().As<IPanelLoginService>();
        builder.RegisterType<PanelLoginValidatior>().As<IValidator<PanelLoginVM>>();

        builder.RegisterType<UtilsService>().As<IUtilsService>();

        builder.RegisterType<HomeSliderService>().As<IHomeSliderService>();
        builder.RegisterType<HomeSliderValidatior>().As<IValidator<HomeSliderVM>>();

        builder.RegisterType<AboutService>().As<IAboutService>();
        builder.RegisterType<AboutOurWhoService>().As<IAboutOurWhoService>();

        builder.RegisterType<ServicesService>().As<IServicesService>();
        builder.RegisterType<ServiceValidatior>().As<IValidator<ServiceVM>>();

        builder.RegisterType<ProjectService>().As<IProjectService>();
        builder.RegisterType<ProjectValidatior>().As<IValidator<ProjectVM>>();

        builder.RegisterType<AboutSubTableValidatior>().As<IValidator<AboutSubTableVM>>();
        builder.RegisterType<AboutSubTableService>().As<IAboutSubTableService>();

        builder.RegisterType<HomeProjectWorkService>().As<IHomeProjectWorkService>();
        builder.RegisterType<HomeProjectWorkValidatior>().As<IValidator<HomeProjectWorkVM>>();

        builder.RegisterType<HomeWhySorswareService>().As<IHomeWhySorswareService>();
        builder.RegisterType<HomeWhySorswareValidatior>().As<IValidator<HomeWhySorswareVM>>();

        builder.RegisterType<ContactInfoService>().As<IContactInfoService>();
        builder.RegisterType<ContactInfoValidatior>().As<IValidator<ContactInfoVM>>();

        builder.RegisterType<PersonalService>().As<IPersonalService>();
        builder.RegisterType<PersonalValidatior>().As<IValidator<PersonalVM>>();

        builder.RegisterType<ProfileService>().As<IProfileService>();

        builder.RegisterType<UserService>().As<IUserService>();

        builder.RegisterType<FileImageService>().As<IFileImageService>();

        builder.RegisterType<ContactInformationService>().As<IContactInformationService>();
        builder.RegisterType<ContactInformationValidatior>().As<IValidator<ContactInformationVM>>();

        builder.RegisterType<HomeWhatAreWeDoingService>().As<IHomeWhatAreWeDoingService>();
    }
}