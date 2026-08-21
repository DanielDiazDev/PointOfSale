using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Branches;

public class GetBranches
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBranches(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetBranchResponse>>> Execute()
    {
        var branches = await _unitOfWork.Branches.GetAllAsync();
        if (!branches.Any())
        {
            return Result<List<GetBranchResponse>>.Failure("No branches found");
        }

        var branchesResponse = branches.Select(b => new GetBranchResponse(b.Id.Value, b.Name)).ToList();

        return Result<List<GetBranchResponse>>.Success(branchesResponse);
    }
}
