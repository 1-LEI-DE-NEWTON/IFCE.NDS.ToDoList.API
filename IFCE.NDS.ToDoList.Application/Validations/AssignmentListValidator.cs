using FluentValidation;
using NDS_ToDo.Domain.Entities;

namespace NDS_ToDo.Application.Validations;

public class AssignmentListValidator : AbstractValidator<AssignmentList>
{
    public AssignmentListValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEqual(Guid.Empty);
    }
}