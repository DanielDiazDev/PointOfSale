namespace PointOfSale.Application.Products;

public record GetProductResponse(Guid Id, string Name, decimal Price, decimal Cost, int Stock, Guid CategoryId,
    bool CanMarkup, decimal MarkupPercentage , Guid SupplierId, Guid BranchId);