using FluentValidation;
using Viseralbug.Models;

namespace Viseralbug.Validators
{
    public class ProjectValidator : AbstractValidator<Project>
    {
        public ProjectValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Project name is required")
                .Length(1, 100).WithMessage("Project name must be between 1 and 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Project description is required")
                .Length(1, 500).WithMessage("Project description must be between 1 and 500 characters");

            RuleFor(x => x.Status)
                .Must(status => status == null || new[] { "Active", "Inactive", "Completed", "On Hold" }.Contains(status))
                .WithMessage("Status must be Active, Inactive, Completed, or On Hold");
        }
    }
}
