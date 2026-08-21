using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Model.Orders;
using PointOfSale.Model.Suppliers;

namespace PointOfSale.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> e)
    {
        e.HasKey(o => o.Id);
        e.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new OrderId(value));
       

        e.HasMany(o => o.OrderDetails)
            .WithOne()
            .HasForeignKey(od => od.OrderId);
        e.Property(x => x.SupplierId)
            .HasConversion(
                id => id.Value,
                value => new SupplierId(value));
        e.HasOne<Supplier>()
            .WithMany()
            .HasForeignKey(o => o.SupplierId);
        
    }
}