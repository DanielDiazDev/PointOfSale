using PointOfSale.Model.Categories;
using PointOfSale.Model.Products;
using PointOfSale.Model.Repositories;
using PointOfSale.Model.Suppliers;
using PointOfSale.Shared;

namespace PointOfSale.Application.Categories.Update;

public class UpdateProduct
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProduct(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(UpdateProductCommand command)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(new ProductId(command.Id));

        if (product is null)
            return Result.Failure("Product not found");

        product.Update(command.name, new Money(command.price), (new Money(command.cost)),  command.stock,
            (new CategoryId(command.categoryId))
        , new MarkupPolicy(command.CanMarkup, command.MarkupPercentage), 
            (new SupplierId(command.SupplierId)), new BranchId(command.BranchId));

        await _unitOfWork.Products.UpdateAsync(product);

        return Result.Success();
    }
}