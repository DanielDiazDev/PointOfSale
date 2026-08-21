using PointOfSale.Model.Users;

namespace PointOfSale.Application.Users.Update;

public record UpdateUserCommand(Guid Id, string Name, string Password, int Role);
