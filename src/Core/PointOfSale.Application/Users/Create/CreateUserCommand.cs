using PointOfSale.Model.Users;

namespace PointOfSale.Application.Users.Create;

public record CreateUserCommand(string Name, string Password, int Role);
