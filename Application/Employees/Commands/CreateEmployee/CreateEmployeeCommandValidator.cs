using FluentValidation;

namespace Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(v => v.IsMale)
            .NotEmpty();
    }
}