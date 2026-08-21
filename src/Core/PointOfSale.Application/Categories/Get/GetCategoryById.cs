using PointOfSale.Model.Categories;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Categories.Get;

public class GetCategoryById
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoryById(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<GetCategoryResponse>> Execute(GetCategoryByIdQuery query)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(new CategoryId(query.Id));

        if (category is null)
            return Result<GetCategoryResponse>.Failure("Category not found");

        var response = new GetCategoryResponse(category.Id.Value, category.Name);

        return Result<GetCategoryResponse>.Success(response);
    }
}

// record CategoryDto(Guid Id, string Name);