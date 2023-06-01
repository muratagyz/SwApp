using FluentValidation;
using SwAppViewModel.Panel;

namespace SwAppService.Validation;

public class PersonalValidatior : AbstractValidator<PersonalVM>
{
    public PersonalValidatior()
    {
        RuleFor(x => x.PersonalFullName).NotEmpty();
        RuleFor(x => x.PersonalPosition).NotEmpty();
    }
}