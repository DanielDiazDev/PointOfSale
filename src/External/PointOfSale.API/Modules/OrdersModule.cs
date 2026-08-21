using PointOfSale.Application.Orders;
using PointOfSale.Application.Orders.AddOrderDetail;
using PointOfSale.Application.Orders.Confirm;
using PointOfSale.Application.Orders.Create;
using PointOfSale.Application.Orders.Delete;
using PointOfSale.Application.Orders.Get;
using PointOfSale.Application.Orders.RemoveOrderDetail;
using PointOfSale.Model.Repositories;

namespace PointOfSale.API.Modules;

public class OrdersModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/orders").WithTags("Orders");

        group.MapGet("/", async (IUnitOfWork unitOfWork) =>
        {
            var getOrders = new GetOrders(unitOfWork);
            var result = await getOrders.Execute();
            return result;
        });

        group.MapPost("/create", async (IUnitOfWork unitOfWork, CreateOrderCommand createOrderCommand) =>
        {
            var createOrder = new CreateOrder(unitOfWork);
            var result = await createOrder.Execute(createOrderCommand);
            return result;
        });

        group.MapGet("/{id}", async (IUnitOfWork unitOfWork, Guid id) =>
        {
            var getOrder = new GetOrderById(unitOfWork);
            var result = await getOrder.Execute(new GetOrderByIdQuery(id));
            return result;
        });

        group.MapDelete("/{id}", async (IUnitOfWork unitOfWork, Guid id) =>
        {
            var deleteOrder = new DeleteOrder(unitOfWork);
            var result = await deleteOrder.Execute(id);
            return result;
        });


        group.MapPost("/add-detail", async (IUnitOfWork unitOfWork, AddOrderDetailCommand addOrderDetailCommand) =>
        {
            var addOrderDetail = new AddOrderDetail(unitOfWork);
            var result = await addOrderDetail.Execute(addOrderDetailCommand);
            return result;
        });

        group.MapDelete("/remove-detail", async (IUnitOfWork unitOfWork, Guid orderId, Guid detailId) =>
        {
            var removeOrderDetail = new RemoveOrderDetail(unitOfWork);
            var result = await removeOrderDetail.Execute(orderId, detailId);
            return result;
        });
        group.MapPost("/confirm", async (IUnitOfWork unitOfWork, ConfirmOrderCommand confirmOrderCommand) =>
        {
            var confirmOrder = new ConfirmOrder(unitOfWork);
            var result = await confirmOrder.Execute(confirmOrderCommand);
            return result;
        });
    }
}