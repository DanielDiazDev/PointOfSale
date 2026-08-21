using PointOfSale.Model.Orders;
using PointOfSale.Model.Repositories;
using PointOfSale.Model.Sales;
using PointOfSale.Shared;

namespace PointOfSale.Application.Sales.Confirm;

public class ConfirmSale
{

    private readonly IUnitOfWork _unitOfWork;

    public ConfirmSale(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(ConfirmSaleCommand command)
    {
        var sale = await _unitOfWork.Sales.GetByIdAsync(new SaleId(command.SaleId),  s =>s.SaleDetails);

        if (sale is null)
            return Result.Failure("Sale not found");

        foreach (var detail in sale.SaleDetails)
        {
            
            var product = await _unitOfWork.Products.GetByIdAsync(detail.ProductId);
            
            if (product is null)
                return Result.Failure($"Product {detail.ProductId.Value} not found");
            //
            var result = product.DecreaseStock(detail.Quantity);
            if (!result.IsSuccess)
                return result;
            
            await _unitOfWork.Products.UpdateAsync(product);
            
        }

        await _unitOfWork.Sales.UpdateAsync(sale);

        return Result.Success();
    }
}