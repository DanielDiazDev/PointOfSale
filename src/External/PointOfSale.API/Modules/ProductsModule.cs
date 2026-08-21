using PointOfSale.Application.Categories;
using PointOfSale.Application.Categories.CreateCategory;
using PointOfSale.Application.Categories.Delete;
using PointOfSale.Application.Categories.Get;
using PointOfSale.Application.Categories.Update;
using PointOfSale.Application.Products;
using PointOfSale.Model.Repositories;

namespace PointOfSale.API.Modules;

public class ProductsModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products").WithTags("Products");

        group.MapGet("/", async (IUnitOfWork unitOfWork) =>
        {
            var getCategories = new GetCategories(unitOfWork);
            var result = await getCategories.Execute();
            return result;
        });
        
        group.MapPost("/create", async (IUnitOfWork unitOfWork, CreateProductCommand  createProductCommand) =>
        {
            
            var createProduct = new CreateProduct(unitOfWork);
            var result = await createProduct.Execute(createProductCommand);
           
            return result;
        });

        group.MapGet("/{id}", async (IUnitOfWork unitOfWork, Guid id) =>
        {
            var getProductById = new GetProductById(unitOfWork);
            var result = await getProductById.Execute(new GetProductByIdQuery(id));

            return result;
        });
        group.MapDelete("/{id}", async (IUnitOfWork unitOfWork, Guid id) =>
        {
            var deleteProduct = new DeleteProduct(unitOfWork);
            var result = await deleteProduct.Execute(id);
            return result;
        });
        group.MapPut("/", async (IUnitOfWork unitOfWork, UpdateProductCommand updateProductCommand) =>
        {
            var updateProduct = new UpdateProduct(unitOfWork);
            var result = await updateProduct.Execute(updateProductCommand);
            return result;
        });
    }
}