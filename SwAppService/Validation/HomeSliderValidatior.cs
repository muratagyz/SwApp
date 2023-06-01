using FluentValidation;
using SwAppViewModel.Panel;

namespace SwAppService.Validation;

public class HomeSliderValidatior : AbstractValidator<HomeSliderVM>
{
    public HomeSliderValidatior()
    {
        RuleFor(x => x.HomeSliderTitle).NotEmpty();
        RuleFor(x => x.HomeSliderDetails).NotEmpty();
    }
}