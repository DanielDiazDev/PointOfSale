using PointOfSale.Application.Orders.Get;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Orders;

public class GetOrders
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrders(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetOrderResponse>>> Execute()
    {
        var orders = await _unitOfWork.Orders.GetAllAsync(o => o.OrderDetails);
        if (!orders.Any())
        {
            return Result<List<GetOrderResponse>>.Failure("No orders found");
        }

        var allDetails = orders.SelectMany(o => o.OrderDetails).ToList();

        var ordersResponse = orders.Select(o =>
        {
            var details = allDetails
                .Where(d => d.OrderId == o.Id)
                .Select(d => new GetOrderDetailResponse(d.Id.Value, d.NewStock, d.ProductId.Value, d.DateCreated,
                    d.OrderId.Value))
                .ToList();

            return new GetOrderResponse(o.Id.Value, o.SupplierId.Value, details);
        }).ToList();

        return Result<List<GetOrderResponse>>.Success(ordersResponse);
    }
}
