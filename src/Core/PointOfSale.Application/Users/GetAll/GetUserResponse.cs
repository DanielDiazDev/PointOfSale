using PointOfSale.Model.Users;

namespace PointOfSale.Application.Users;

public record GetUserResponse(Guid Id, string Name, User.UserRole Role);
