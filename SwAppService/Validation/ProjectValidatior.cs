using FluentValidation;
using SwAppViewModel.Panel;

namespace SwAppService.Validation;

public class ProjectValidatior : AbstractValidator<ProjectVM>
{
    public ProjectValidatior()
    {
        RuleFor(x => x.ProjectUrl).NotEmpty();
        RuleFor(x => x.ProjectDetails).NotEmpty();
        RuleFor(x => x.ProjectCategory).NotEmpty();
    }
}