using PointOfSale.Model.Repositories;
using PointOfSale.Model.Users;
using PointOfSale.Shared;

namespace PointOfSale.Application.Users.Get;

public class GetUserById
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserById(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<GetUserResponse>> Execute(GetUserByIdQuery query)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(new UserId(query.Id));

        if (user is null)
            return Result<GetUserResponse>.Failure("User not found");

        var response = new GetUserResponse(user.Id.Value, user.Name, user.Role);

        return Result<GetUserResponse>.Success(response);
    }
}
