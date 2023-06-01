using FluentValidation;
using SwAppViewModel.Panel;

namespace SwAppService.Validation;

public class AboutSubTableValidatior : AbstractValidator<AboutSubTableVM>
{
    public AboutSubTableValidatior()
    {
        RuleFor(x => x.AboutSubTableTitle).NotEmpty();
        RuleFor(x => x.AboutSubTableDetails).NotEmpty();
    }
}