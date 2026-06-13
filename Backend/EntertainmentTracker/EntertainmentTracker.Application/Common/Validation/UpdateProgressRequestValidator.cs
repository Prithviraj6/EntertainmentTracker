using EntertainmentTracker.Application.Animes.DTOs;
using FluentValidation;

namespace EntertainmentTracker.Application.Common.Validation
{
    public sealed class UpdateProgressRequestValidator
    : AbstractValidator<UpdateProgressRequest>
    {
        public UpdateProgressRequestValidator()
        {
            RuleFor(x => x.Progress)
                .InclusiveBetween(0, 100);
        }
    }
}
