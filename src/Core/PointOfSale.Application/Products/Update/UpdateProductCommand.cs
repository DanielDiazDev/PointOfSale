using PointOfSale.Model.Categories;
using PointOfSale.Model.Products;
using PointOfSale.Model.Suppliers;

namespace PointOfSale.Application.Categories.Update;

public record UpdateProductCommand(Guid Id, string name, decimal price, decimal cost, int stock, Guid categoryId,
     bool CanMarkup, decimal MarkupPercentage, Guid SupplierId, Guid BranchId);