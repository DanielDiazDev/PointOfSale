using PointOfSale.Model.Orders;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Orders.RemoveOrderDetail;

public class RemoveOrderDetail
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveOrderDetail(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(Guid orderId, Guid orderDetailId)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(new OrderId(orderId), o => o.OrderDetails);

        if (order is null)
            return Result.Failure("Order not found");

        var detail = order.OrderDetails.FirstOrDefault(od => od.Id == new OrderDetailId(orderDetailId));

        if (detail is null || detail.OrderId != order.Id)
            return Result.Failure("Order detail not found");

        order.RemoveOrderDetail(detail);

        await _unitOfWork.Orders.UpdateAsync(order);

        return Result.Success();
    }
}
