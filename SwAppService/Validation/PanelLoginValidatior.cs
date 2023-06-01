using FluentValidation;
using SwAppViewModel.Panel;

namespace SwAppService.Validation;

public class PanelLoginValidatior : AbstractValidator<PanelLoginVM>
{
    public PanelLoginValidatior()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}