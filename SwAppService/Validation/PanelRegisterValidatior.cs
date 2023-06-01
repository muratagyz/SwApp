using FluentValidation;
using SwAppViewModel.Panel;

namespace SwAppService.Validation;

public class PanelRegisterValidatior : AbstractValidator<PanelRegisterVM>
{
    public PanelRegisterValidatior()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}