using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Model.Categories;
using PointOfSale.Model.Products;
using PointOfSale.Model.Suppliers;

namespace PointOfSale.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> e)
    {
        e.HasKey(x => x.Id);
        e.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new ProductId(value));
        e.Property(x => x.CategoryId)
            .HasConversion(
                id => id.Value,
                value => new CategoryId(value));
        e.HasOne<Category>()
            .WithMany()
            .HasForeignKey(p => p.CategoryId);

        e.HasOne<Supplier>()
            .WithMany()
            .HasForeignKey(p => p.SupplierId);

        e.HasOne<Branch>()
            .WithMany()
            .HasForeignKey(p => p.BranchId);
        
        e.OwnsOne(p => p.MarkupPolicy, mp =>
        {
            mp.Property(m => m.CanMarkup);
            mp.Property(m => m.MarkupPercentage);
        });
       
        e.OwnsOne(p => p.Price, mp =>
        {
            mp.Property(m => m.Value)
                .HasColumnName("Price")
                .HasPrecision(18, 2);
        });

        e.OwnsOne(p => p.Cost, mp =>
        {
            mp.Property(m => m.Value)
                .HasColumnName("Cost")
                .HasPrecision(18, 2);
        });
    }
}