using FluentValidation;

namespace Ratting.Application.Players.Queries;

public class GetPlayerDetailsValidator: AbstractValidator<GetPlayerDetailsQuery>
{
    public GetPlayerDetailsValidator()
    {
        RuleFor(player => player.PlayerId)
            .NotEqual(Guid.Empty);
    }
}