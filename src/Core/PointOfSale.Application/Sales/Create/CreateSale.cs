using PointOfSale.Model.Customers;
using PointOfSale.Model.Products;
using PointOfSale.Model.Repositories;
using PointOfSale.Model.Sales;
using PointOfSale.Shared;

namespace PointOfSale.Application.Sales.Create;

public class CreateSale
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSale(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<Guid>> Execute(CreateSaleCommand command)
    {
        var sale = Sale.Create(new CustomerId(command.CustomerId), new ProductId(command.ProductId));
        if (!sale.IsSuccess)
        {
            return Result<Guid>.Failure(sale.Error);
        }

        await _unitOfWork.Sales.AddAsync(sale.Value);

        return Result<Guid>.Success(sale.Value.Id.Value);
    }
}
