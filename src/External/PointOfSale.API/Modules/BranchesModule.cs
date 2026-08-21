using PointOfSale.Application.Branches;
using PointOfSale.Application.Branches.Create;
using PointOfSale.Application.Branches.Delete;
using PointOfSale.Application.Branches.Get;
using PointOfSale.Application.Branches.Update;
using PointOfSale.Model.Repositories;

namespace PointOfSale.API.Modules;

public class BranchesModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/branches").WithTags("Branches");

        group.MapGet("/", async (IUnitOfWork unitOfWork) =>
        {
            var getBranches = new GetBranches(unitOfWork);
            var result = await getBranches.Execute();
            return result;
        });

        group.MapPost("/create", async (IUnitOfWork unitOfWork, CreateBranchCommand createBranchCommand) =>
        {
            var createBranch = new CreateBranch(unitOfWork);
            var result = await createBranch.Execute(createBranchCommand);
            return result;
        });

        group.MapGet("/{id}", async (IUnitOfWork unitOfWork, Guid id) =>
        {
            var getBranch = new GetBranchById(unitOfWork);
            var result = await getBranch.Execute(new GetBranchByIdQuery(id));
            return result;
        });

        group.MapDelete("/{id}", async (IUnitOfWork unitOfWork, Guid id) =>
        {
            var deleteBranch = new DeleteBranch(unitOfWork);
            var result = await deleteBranch.Execute(id);
            return result;
        });

        group.MapPut("/", async (IUnitOfWork unitOfWork, UpdateBranchCommand updateBranchCommand) =>
        {
            var updateBranch = new UpdateBranch(unitOfWork);
            var result = await updateBranch.Execute(updateBranchCommand);
            return result;
        });
    }
}