using PointOfSale.Model.Orders;
using PointOfSale.Model.Repositories;
using PointOfSale.Model.Suppliers;
using PointOfSale.Shared;

namespace PointOfSale.Application.Orders.Create;

public class CreateOrder
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrder(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<Guid>> Execute(CreateOrderCommand command)
    {
        var order = Order.Create(new SupplierId(command.SupplierId));
        if (!order.IsSuccess)
        {
            return Result<Guid>.Failure(order.Error);
        }

        await _unitOfWork.Orders.AddAsync(order.Value);

        return Result<Guid>.Success(order.Value.Id.Value);
    }
}
