using FluentValidation;

namespace Ratting.Application.Players.Commands.CreatePlayer;

public class CreatePlayerCommandValidator: AbstractValidator<CreatePlayerCommand>
{
    public CreatePlayerCommandValidator()
    {
        RuleFor(player =>
                player.Name)
            .NotEmpty()
            .MaximumLength(250)
            .MinimumLength(5);
    }
}