using FluentValidation;

namespace Ratting.Aplication.Players.Commands.UpdatePlayer;

public class UpdatePlayerCommandValidator: AbstractValidator<UpdatePlayerCommand>
{
    public UpdatePlayerCommandValidator()
    {
        RuleFor(player => player.Name)
            .NotEmpty()
            .MaximumLength(250);
        RuleFor(player => player.UserId)
            .NotEqual(Guid.Empty);
        RuleFor(player => player.BestResult)
            .GreaterThanOrEqualTo(0);
    }
}