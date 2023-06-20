using FluentValidation;

namespace Application.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(v => v.IsMale)
            .NotEmpty();
    }
}