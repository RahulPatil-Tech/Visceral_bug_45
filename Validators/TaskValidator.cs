using FluentValidation;
using Viseralbug.Models;

namespace Viseralbug.Validators
{
    public class TaskValidator : AbstractValidator<WorkTask>
    {
        public TaskValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Task title is required")
                .Length(1, 200).WithMessage("Task title must be between 1 and 200 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Task description is required")
                .Length(1, 1000).WithMessage("Task description must be between 1 and 1000 characters");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Task status is required")
                .Must(status => new[] { "To Do", "In Progress", "Review", "Done", "Cancelled" }.Contains(status))
                .WithMessage("Status must be To Do, In Progress, Review, Done, or Cancelled");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("Project ID must be greater than 0");

            RuleFor(x => x.CreatedById)
                .GreaterThan(0).WithMessage("Created By ID must be greater than 0");
        }
    }
}
