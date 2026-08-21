using PointOfSale.Model.Categories;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Categories.CreateCategory;

public class CreateCategory
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategory(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<Result<Guid>> Execute(CreateCategoryCommand command)
    {
        var category = Category.Create(command.Name);
        if (!category.IsSuccess)
        {
            return Result<Guid>.Failure(category.Error);
        }

        await _unitOfWork.Categories.AddAsync(category.Value);

        return Result<Guid>.Success(category.Value.Id.Value);
    }
}