using PointOfSale.Application.Suppliers;
using PointOfSale.Application.Suppliers.Create;
using PointOfSale.Application.Suppliers.Delete;
using PointOfSale.Application.Suppliers.Get;
using PointOfSale.Application.Suppliers.Update;
using PointOfSale.Model.Repositories;

namespace PointOfSale.API.Modules;

public class SuppliersModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/suppliers").WithTags("Suppliers");

        group.MapGet("/", async (IUnitOfWork unitOfWork) =>
        {
            var getSuppliers = new GetSuppliers(unitOfWork);
            var result = await getSuppliers.Execute();
            return result;
        });

        group.MapPost("/create", async (IUnitOfWork unitOfWork, CreateSupplierCommand createSupplierCommand) =>
        {
            var createSupplier = new CreateSupplier(unitOfWork);
            var result = await createSupplier.Execute(createSupplierCommand);
            return result;
        });

        group.MapGet("/{id}", async (IUnitOfWork unitOfWork, Guid id) =>
        {
            var getSupplier = new GetSupplierById(unitOfWork);
            var result = await getSupplier.Execute(new GetSupplierByIdQuery(id));
            return result;
        });

        group.MapDelete("/{id}", async (IUnitOfWork unitOfWork, Guid id) =>
        {
            var deleteSupplier = new DeleteSupplier(unitOfWork);
            var result = await deleteSupplier.Execute(id);
            return result;
        });

        group.MapPut("/", async (IUnitOfWork unitOfWork, UpdateSupplierCommand updateSupplierCommand) =>
        {
            var updateSupplier = new UpdateSupplier(unitOfWork);
            var result = await updateSupplier.Execute(updateSupplierCommand);
            return result;
        });
    }
}