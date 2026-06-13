using EntertainmentTracker.Application.Auth.DTOs;
using FluentValidation;

namespace EntertainmentTracker.Application.Auth.Validators
{
    public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(254)
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
