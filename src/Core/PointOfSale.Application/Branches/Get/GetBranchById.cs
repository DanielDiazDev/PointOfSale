using PointOfSale.Model.Products;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Branches.Get;

public class GetBranchById
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBranchById(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<GetBranchResponse>> Execute(GetBranchByIdQuery query)
    {
        var branch = await _unitOfWork.Branches.GetByIdAsync(new BranchId(query.Id));

        if (branch is null)
            return Result<GetBranchResponse>.Failure("Branch not found");

        var response = new GetBranchResponse(branch.Id.Value, branch.Name);

        return Result<GetBranchResponse>.Success(response);
    }
}
