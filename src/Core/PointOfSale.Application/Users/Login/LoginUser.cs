using PointOfSale.Model.Repositories;
using PointOfSale.Model.Services;
using PointOfSale.Model.Users;
using PointOfSale.Shared;

namespace PointOfSale.Application.Users.Login;

public class LoginUser
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public LoginUser(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _tokenService = tokenService;
    }

    public async Task<Result<string>> Execute(LoginUserCommand command)
    {
        var user = await _unitOfWork.Users.GetByNameAsync(command.Name);
        if (user is null)
            return Result<string>.Failure("User not found.");

        var isValid = _passwordHasher.Verify(command.Password, user.Password);
        if (!isValid)
            return Result<string>.Failure("Password doesn't match.");
        var response = _tokenService.GenerateToken(user.Id.Value, user.Name, user.Role.ToString());
        return Result<string>.Success(response);
    }
}