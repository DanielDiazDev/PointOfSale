using PointOfSale.Model.Categories;
using PointOfSale.Model.Products;
using PointOfSale.Model.Repositories;
using PointOfSale.Model.Suppliers;
using PointOfSale.Shared;

namespace PointOfSale.Application.Products;

public class CreateProduct
{
    private IUnitOfWork _unitOfWork;

    public CreateProduct(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Execute(CreateProductCommand createProductCommand)
    {
        var markupPolicy = new MarkupPolicy(createProductCommand.CanMarkup, createProductCommand.MarkupPercentage);
        var product = Product.Create(
            createProductCommand.Name,
            new Money(MarkupPolicyService.GetPriceFinal(createProductCommand.Cost, createProductCommand.Price, createProductCommand.CanMarkup,
                createProductCommand.MarkupPercentage
                )),
            new Money(createProductCommand.Cost),
            createProductCommand.Stock,
            new CategoryId(createProductCommand.CategoryId),
            markupPolicy,
            new SupplierId(createProductCommand.SupplierId),
            new BranchId(createProductCommand.BranchId)
        );
        if (!product.IsSuccess)
        {
            return Result<Guid>.Failure(product.Error);
        }
       
       
        await _unitOfWork.Products.AddAsync(product.Value);

       
        return Result<Guid>.Success(product.Value.Id.Value);
    }

    
}