using PointOfSale.Model.Orders;
using PointOfSale.Model.Products;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Orders.AddOrderDetail;

public class AddOrderDetail
{
    private readonly IUnitOfWork _unitOfWork;

    public AddOrderDetail(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(AddOrderDetailCommand command)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(new OrderId(command.OrderId), o => o.OrderDetails);

        if (order is null)
            return Result.Failure("Order not found");

        var result = order.AddOrderDetail(command.NewStock, new ProductId(command.ProductId));
        if (!result.IsSuccess)
            return result;

        await _unitOfWork.Orders.UpdateAsync(order);

        return Result.Success();
    }
}
