using PointOfSale.Model.Primitives;
using PointOfSale.Shared;

namespace PointOfSale.Model.Products;

public class Branch
{
    public BranchId Id { get; private set; }
    public string Name { get; private set; }
    
    protected Branch() { }

    private Branch(BranchId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Result<Branch> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Branch>.Failure("Name cannot be empty.");
        }

        var branch = new Branch(new BranchId(Guid.NewGuid()), name);
        return Result<Branch>.Success(branch);
    }

    public static Result<Branch> Seed(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Branch>.Failure("Name cannot be empty.");
        }

        var branch = new Branch(new BranchId(id), name);
        return Result<Branch>.Success(branch);
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