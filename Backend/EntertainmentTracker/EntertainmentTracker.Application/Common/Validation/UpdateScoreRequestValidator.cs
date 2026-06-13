using EntertainmentTracker.Application.Animes.DTOs;
using FluentValidation;

namespace EntertainmentTracker.Application.Common.Validation
{
    public sealed class UpdateScoreRequestValidator
    : AbstractValidator<UpdateScoreRequest>
    {
        public UpdateScoreRequestValidator()
        {
            RuleFor(x => x.Score)
                .InclusiveBetween(1, 10);
        }
    }
}
