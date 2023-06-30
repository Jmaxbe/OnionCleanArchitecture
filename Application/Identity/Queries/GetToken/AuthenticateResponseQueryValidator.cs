using FluentValidation;

namespace Application.Identity.Queries.GetToken;

public class AuthenticateResponseQueryValidator : AbstractValidator<AuthenticateResponseQuery>
{
    public AuthenticateResponseQueryValidator()
    {
        When(v => string.IsNullOrEmpty(v.Email), () =>
        {
            RuleFor(f => f.UserName).NotNull()
                .Length(2, 256);
        });
        When(v => string.IsNullOrEmpty(v.UserName), () =>
        {
            RuleFor(f => f.Email).Length(6, 256)
                .NotNull()
                .EmailAddress();
        });
        RuleFor(v => v.Password)
            .Length(4, 256)
            .NotNull();
    }
}