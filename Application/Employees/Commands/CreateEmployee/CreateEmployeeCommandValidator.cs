using FluentValidation;

namespace Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(v => v.IsMale).NotEmpty();
        RuleFor(v => v.FirstName)
            .Length(2, 256)
            .NotEmpty();
        RuleFor(v => v.LastName)
            .Length(2, 256)
            .NotEmpty();
        RuleFor(v => v.MiddleName)
            .Length(2, 256);
        RuleFor(v => v.UserRoles)
            .NotEmpty();
        RuleFor(v => v.Password)
            .MinimumLength(3)
            .NotEmpty();
        RuleFor(v => v.Email)
            .EmailAddress();
        RuleFor(v => v.UserName)
            .Length(2, 256)
            .NotEmpty();
        RuleFor(v => v.HireDate).NotEmpty();
        RuleFor(v => v.BirthDate)
            .LessThan(DateTime.Now)
            .NotEmpty();
    }
}