using EntertainmentTracker.Application.Animes.DTOs;
using FluentValidation;

namespace EntertainmentTracker.Application.Common.Validation
{
    public sealed class AddUserAnimeRequestValidator
    : AbstractValidator<AddUserAnimeRequest>
    {
        public AddUserAnimeRequestValidator()
        {
            RuleFor(x => x.AnimeId)
                .NotEmpty();
        }
    }
}
