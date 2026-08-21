using PointOfSale.Model.Primitives;
using PointOfSale.Model.Products;
using PointOfSale.Shared;

namespace PointOfSale.Model.Categories;

public class Category
{
    public CategoryId Id { get; private set; }
    public string Name { get; private set; }

    protected Category() { }

    private Category(CategoryId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Result<Category> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Category>.Failure("Name cannot be empty.");
        }

        var category = new Category(new CategoryId(Guid.NewGuid()), name);
        return Result<Category>.Success(category);
    }

    public static Result<Category> Seed(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Category>.Failure("Name cannot be empty.");
        }

        var category = new Category(new CategoryId(id), name);
        return Result<Category>.Success(category);
    }

    public Result Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure("Name cannot be empty.");
        }

        Name = name;
        return Result.Success();
    }
}