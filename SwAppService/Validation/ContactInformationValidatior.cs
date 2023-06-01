using FluentValidation;
using SwAppViewModel.Panel;

namespace SwAppService.Validation;

public class ContactInformationValidatior : AbstractValidator<ContactInformationVM>
{
    public ContactInformationValidatior()
    {
        RuleFor(x => x.ContactInformationTitle).NotEmpty();
        RuleFor(x => x.ContactInformationAddress).NotEmpty();
    }
}