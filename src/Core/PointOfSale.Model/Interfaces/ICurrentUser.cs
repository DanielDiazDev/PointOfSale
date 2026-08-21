using PointOfSale.Model.Users;

namespace PointOfSale.Model.Repositories;

public interface ICurrentUser
{
    Guid? UserId { get; }

    string? Username { get; }

    User.UserRole? Role { get; }

    bool IsAuthenticated { get; }
}