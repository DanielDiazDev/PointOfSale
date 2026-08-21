using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Users;

public class GetUsers
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUsers(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetUserResponse>>> Execute()
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        if (!users.Any())
        {
            return Result<List<GetUserResponse>>.Failure("No users found");
        }

        var usersResponse = users.Select(u => new GetUserResponse(u.Id.Value, u.Name, u.Role)).ToList();

        return Result<List<GetUserResponse>>.Success(usersResponse);
    }
}
