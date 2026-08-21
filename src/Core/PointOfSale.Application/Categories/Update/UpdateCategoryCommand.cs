using PointOfSale.Model.Categories;

namespace PointOfSale.Application.Categories.Update;

public record UpdateCategoryCommand(Guid Id, string Name);