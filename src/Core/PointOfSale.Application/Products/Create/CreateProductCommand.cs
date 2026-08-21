using PointOfSale.Model.Categories;
using PointOfSale.Model.Products;
using PointOfSale.Model.Suppliers;

namespace PointOfSale.Application.Products;

public record CreateProductCommand(string Name, decimal Price, decimal Cost, int Stock, Guid CategoryId,
    bool CanMarkup, decimal MarkupPercentage, Guid SupplierId, Guid BranchId);