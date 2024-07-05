using FluentValidation;

namespace Ratting.Application.Players.Commands.CreatePlayer;

public class CreatePlayerCommandValidator: AbstractValidator<CreatePlayerCommand>
{
    public CreatePlayerCommandValidator()
    {
        RuleFor(player =>
                player.Name)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(20);
    }
}