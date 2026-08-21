using PointOfSale.Model.Products;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Branches.Create;

public class CreateBranch
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBranch(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<Guid>> Execute(CreateBranchCommand command)
    {
        var branch = Branch.Create(command.Name);
        if (!branch.IsSuccess)
        {
            return Result<Guid>.Failure(branch.Error);
        }

        await _unitOfWork.Branches.AddAsync(branch.Value);

        return Result<Guid>.Success(branch.Value.Id.Value);
    }
}
