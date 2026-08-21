using PointOfSale.Application.Customers;
using PointOfSale.Application.Customers.Create;
using PointOfSale.Application.Customers.Delete;
using PointOfSale.Application.Customers.Get;
using PointOfSale.Application.Customers.Update;
using PointOfSale.Model.Repositories;

namespace PointOfSale.API.Modules;

public class CustomersModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/customers").WithTags("Customers");

        group.MapGet("/", async (IUnitOfWork unitOfWork) =>
        {
            var getCustomers = new GetCustomers(unitOfWork);
            var result = await getCustomers.Execute();
            return result;
        });

        group.MapPost("/create", async (IUnitOfWork unitOfWork, CreateCustomerCommand createCustomerCommand) =>
        {
            var createCustomer = new CreateCustomer(unitOfWork);
            var result = await createCustomer.Execute(createCustomerCommand);
            return result;
        });

        group.MapGet("/{id}", async (IUnitOfWork unitOfWork, Guid id) =>
        {
            var getCustomer = new GetCustomerById(unitOfWork);
            var result = await getCustomer.Execute(new GetCustomerByIdQuery(id));
            return result;
        });

        group.MapDelete("/{id}", async (IUnitOfWork unitOfWork, Guid id) =>
        {
            var deleteCustomer = new DeleteCustomer(unitOfWork);
            var result = await deleteCustomer.Execute(id);
            return result;
        });

        group.MapPut("/", async (IUnitOfWork unitOfWork, UpdateCustomerCommand updateCustomerCommand) =>
        {
            var updateCustomer = new UpdateCustomer(unitOfWork);
            var result = await updateCustomer.Execute(updateCustomerCommand);
            return result;
        });
    }
}