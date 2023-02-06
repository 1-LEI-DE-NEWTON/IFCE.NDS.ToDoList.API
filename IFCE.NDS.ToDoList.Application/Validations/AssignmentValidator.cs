using FluentValidation;
using NDS_ToDo.Domain.Entities;

namespace NDS_ToDo.Application.Validations;

public class AssignmentValidator : AbstractValidator<Assignment>
{
    public AssignmentValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255)
            .MinimumLength(3);
        RuleFor(x => x.Deadline)
            .GreaterThan(DateTime.Now);
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEqual(Guid.Empty);
    }
}