using PointOfSale.Model.Categories;
using PointOfSale.Model.Products;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Categories.Delete;

public class DeleteProduct
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProduct(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(Guid id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(new ProductId(id));

        if (product is null)
            return Result.Failure("Product not found");

        await _unitOfWork.Products.DeleteAsync(product.Id);

        return Result.Success();
    }
}

