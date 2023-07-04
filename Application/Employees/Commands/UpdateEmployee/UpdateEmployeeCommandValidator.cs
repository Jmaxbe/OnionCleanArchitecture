using FluentValidation;

namespace Application.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
        RuleFor(v => v.IsMale).NotEmpty();
        RuleFor(v => v.FirstName)
            .Length(2, 256)
            .NotEmpty();
        RuleFor(v => v.LastName)
            .Length(2, 256)
            .NotEmpty();
        RuleFor(v => v.MiddleName)
            .Length(2, 256);
        RuleFor(v => v.HireDate).NotEmpty();
        RuleFor(v => v.BirthDate)
            .LessThan(DateTime.Now)
            .NotEmpty();
    }
}