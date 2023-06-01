using FluentValidation;
using SwAppViewModel.Panel;

namespace SwAppService.Validation;

public class HomeProjectWorkValidatior : AbstractValidator<HomeProjectWorkVM>
{
    public HomeProjectWorkValidatior()
    {
        RuleFor(x => x.HomeProjectWorkSubTitle).NotEmpty();
        RuleFor(x => x.HomeProjectWorkSubDetails).NotEmpty();
    }
}