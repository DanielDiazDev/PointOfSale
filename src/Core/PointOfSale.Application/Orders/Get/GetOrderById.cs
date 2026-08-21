using PointOfSale.Model.Orders;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Orders.Get;

public class GetOrderById
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrderById(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<GetOrderResponse>> Execute(GetOrderByIdQuery query)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(new OrderId(query.Id), o => o.OrderDetails);

        if (order is null)
            return Result<GetOrderResponse>.Failure("Order not found");

        var details = order.OrderDetails
            .Select(d => new GetOrderDetailResponse(
                d.Id.Value, 
                d.NewStock, 
                d.ProductId.Value, 
                d.DateCreated,
                d.OrderId.Value
            ))
            .ToList();

        var response = new GetOrderResponse(order.Id.Value, order.SupplierId.Value, details);

        return Result<GetOrderResponse>.Success(response);
    }
}