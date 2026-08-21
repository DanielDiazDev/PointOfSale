using PointOfSale.Model.Orders;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Orders.Delete;

public class DeleteOrder
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrder(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(Guid id)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(new OrderId(id));

        if (order is null)
            return Result.Failure("Order not found");

        await _unitOfWork.Orders.DeleteAsync(order.Id);

        return Result.Success();
    }
}