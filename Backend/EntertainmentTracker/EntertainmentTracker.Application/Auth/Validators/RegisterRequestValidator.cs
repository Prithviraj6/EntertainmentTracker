using EntertainmentTracker.Application.Auth.DTOs;
using FluentValidation;

namespace EntertainmentTracker.Application.Auth.Validators
{
    public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.DisplayName)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(x => x.Handle)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30)
                .Matches(@"^(?!\.)(?!.*\.\.)([a-z0-9._]+)(?<!\.)$")
                .WithMessage(
                    "Handle can contain only lowercase letters, numbers, dots and underscores.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(254)
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(128)
                .Matches(@"[A-Z]")
                .WithMessage(
                    "Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]")
                .WithMessage(
                    "Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]")
                .WithMessage(
                    "Password must contain at least one number.")
                .Matches(@"[\W_]")
                .WithMessage(
                    "Password must contain at least one special character.");
        }
    }
}
