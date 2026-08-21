using PointOfSale.Model.Orders;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Orders.Confirm;

public class ConfirmOrder
{
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmOrder(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(ConfirmOrderCommand command)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(new OrderId(command.OrderId),  o => o.OrderDetails);

        if (order is null)
            return Result.Failure("Order not found");

        foreach (var detail in order.OrderDetails)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(detail.ProductId);

            if (product is null)
                return Result.Failure($"Product {detail.ProductId.Value} not found");

            var result = product.AddStock(detail.NewStock);
            if (!result.IsSuccess)
                return result;

            await _unitOfWork.Products.UpdateAsync(product);
        }
        await _unitOfWork.Orders.UpdateAsync(order);

        return Result.Success();
    }
}