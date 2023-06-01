using FluentValidation;
using SwAppViewModel.Panel;

namespace SwAppService.Validation;

public class HomeWhySorswareValidatior : AbstractValidator<HomeWhySorswareVM>
{
    public HomeWhySorswareValidatior()
    {
        RuleFor(x => x.HomeWhySorswareSubTitle).NotEmpty();
        RuleFor(x => x.HomeWhySorswareSubDetails).NotEmpty();
    }
}