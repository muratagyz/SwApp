using FluentValidation;
using SwAppViewModel.Panel;

namespace SwAppService.Validation;

public class ServiceValidatior : AbstractValidator<ServiceVM>
{
    public ServiceValidatior()
    {
        RuleFor(x => x.ServiceTitle).NotEmpty();
        RuleFor(x => x.ServiceDetails).NotEmpty();
    }
}