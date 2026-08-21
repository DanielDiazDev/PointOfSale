using PointOfSale.Application.Categories;
using PointOfSale.Application.Categories.CreateCategory;
using PointOfSale.Application.Categories.Delete;
using PointOfSale.Application.Categories.Get;
using PointOfSale.Application.Categories.Update;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.API.Modules;

public class CategoriesModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/categories").WithTags("Categories");

        group.MapGet("/", async (IUnitOfWork unitOfWork) =>
        {
            var getCategories = new GetCategories(unitOfWork);
            var result = await getCategories.Execute();
            return result;
        });
        
        group.MapPost("/create", async (IUnitOfWork unitOfWork, CreateCategoryCommand  createCategoryCommand) =>
        {
            
            var createCategory = new CreateCategory(unitOfWork);
            var result = await createCategory.Execute(createCategoryCommand);
           
            return result;
        });

        group.MapGet("/{id}", async (IUnitOfWork unitOfWork, Guid id) =>
        {
            var getCategory = new GetCategoryById(unitOfWork);
            var result = await getCategory.Execute(new GetCategoryByIdQuery(id));

            return result;
        });
        group.MapDelete("/{id}", async (IUnitOfWork unitOfWork, Guid id) =>
        {
            var deleteCategory = new DeleteCategory(unitOfWork);
            var result = await deleteCategory.Execute(id);
            return result;
        });
        group.MapPut("/", async (IUnitOfWork unitOfWork, UpdateCategoryCommand updateCategoryCommand) =>
        {
            var updateCategory = new UpdateCategory(unitOfWork);
            var result = await updateCategory.Execute(updateCategoryCommand);
            return result;
        });


    }
}