using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PointOfSale.Model.Repositories;
using PointOfSale.Model.Users;

namespace PointOfSale.Infrastructure.Auth;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?
            .User
            .Identity?
            .IsAuthenticated ?? false;

    public Guid? UserId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?
                .User
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            return Guid.TryParse(value, out var id)
                ? id
                : null;
        }
    }

    public string? Username =>
        _httpContextAccessor.HttpContext?
            .User
            .FindFirst(ClaimTypes.Name)?
            .Value;

    public User.UserRole? Role
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?
                .User
                .FindFirst(ClaimTypes.Role)?
                .Value;

            return Enum.TryParse<User.UserRole>(value, out var role)
                ? role
                : null;
        }
    }
}