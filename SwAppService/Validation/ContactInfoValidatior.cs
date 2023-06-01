using FluentValidation;
using SwAppViewModel.Panel;

namespace SwAppService.Validation;

public class ContactInfoValidatior : AbstractValidator<ContactInfoVM>
{
    public ContactInfoValidatior()
    {
        RuleFor(x => x.OfficeLocationName).NotEmpty();
        RuleFor(x => x.OfficeLocationNumber).NotEmpty();
    }
}