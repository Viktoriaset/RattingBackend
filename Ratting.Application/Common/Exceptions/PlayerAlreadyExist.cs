namespace Ratting.Application.Common.Exceptions;

public class PlayerAlreadyExist: Exception
{
    public PlayerAlreadyExist(string name) : base($"Player with name: {name} already exist") {}
}