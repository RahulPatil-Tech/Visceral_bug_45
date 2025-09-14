using FluentValidation;
using Viseralbug.Models;

namespace Viseralbug.Validators
{
    public class BugValidator : AbstractValidator<Bug>
    {
        public BugValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Bug title is required")
                .Length(1, 200).WithMessage("Bug title must be between 1 and 200 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Bug description is required")
                .Length(1, 1000).WithMessage("Bug description must be between 1 and 1000 characters");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Bug status is required")
                .Must(status => new[] { "Open", "In Progress", "Resolved", "Closed", "Reopened" }.Contains(status))
                .WithMessage("Status must be Open, In Progress, Resolved, Closed, or Reopened");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("Project ID must be greater than 0");

            RuleFor(x => x.CreatedById)
                .GreaterThan(0).WithMessage("Created By ID must be greater than 0");
        }
    }
}
