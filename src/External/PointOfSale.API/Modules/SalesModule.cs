using PointOfSale.Application.Orders;
using PointOfSale.Application.Orders.AddOrderDetail;
using PointOfSale.Application.Orders.Confirm;
using PointOfSale.Application.Orders.Create;
using PointOfSale.Application.Orders.Delete;
using PointOfSale.Application.Orders.Get;
using PointOfSale.Application.Orders.RemoveOrderDetail;
using PointOfSale.Application.Sales;
using PointOfSale.Application.Sales.AddSaleDetail;
using PointOfSale.Application.Sales.Confirm;
using PointOfSale.Application.Sales.Create;
using PointOfSale.Application.Sales.Delete;
using PointOfSale.Application.Sales.Get;
using PointOfSale.Application.Sales.RemoveSaleDetail;
using PointOfSale.Model.Repositories;

namespace PointOfSale.API.Modules;

public class SalesModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/sales").WithTags("Sales");

        group.MapGet("/", async (IUnitOfWork unitOfWork) =>
        {
            var getSales = new GetSales(unitOfWork);
            var result = await getSales.Execute();
            return result;
        });

        group.MapPost("/create", async (IUnitOfWork unitOfWork, CreateSaleCommand createSaleCommand) =>
        {
            var createSale = new CreateSale(unitOfWork);
            var result = await createSale.Execute(createSaleCommand);
            return result;
        });

        group.MapGet("/{id}", async (IUnitOfWork unitOfWork, Guid id) =>
        {
            var getSaleById = new GetSaleById(unitOfWork);
            var result = await getSaleById.Execute(new GetSaleByIdQuery(id));
            return result;
        });

        group.MapDelete("/{id}", async (IUnitOfWork unitOfWork, Guid id) =>
        {
            var deleteSale = new DeleteSale(unitOfWork);
            var result = await deleteSale.Execute(id);
            return result;
        });


        group.MapPost("/add-detail", async (IUnitOfWork unitOfWork, AddSaleDetailCommand addSaleDetailCommand) =>
        {
            var addSaleDetail = new AddSaleDetail(unitOfWork);
            var result = await addSaleDetail.Execute(addSaleDetailCommand);
            return result;
        });

        group.MapDelete("/remove-detail", async (IUnitOfWork unitOfWork, Guid saleId, Guid detailId) =>
        {
            var removeSaleDetail = new RemoveSaleDetail(unitOfWork);
            var result = await removeSaleDetail.Execute(saleId, detailId);
            return result;
        });
        group.MapPost("/confirm", async (IUnitOfWork unitOfWork, ConfirmSaleCommand confirmSaleCommand) =>
        {
            var confirmSale = new ConfirmSale(unitOfWork);
            var result = await confirmSale.Execute(confirmSaleCommand);
            return result;
        });
    }
}