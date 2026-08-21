using PointOfSale.Model.Products;

namespace PointOfSale.Application.Branches.Update;

public record UpdateBranchCommand(Guid Id, string Name);
