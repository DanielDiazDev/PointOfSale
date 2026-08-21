using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Model.Products;

namespace PointOfSale.Infrastructure.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> e)
    {
        e.HasKey(x => x.Id);
        e.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new BranchId(value));
        e.HasData(
            new
            {
                Id = new BranchId(Guid.Parse("b6cc599f-d5c0-49e6-ba53-afea402faec1")),
                Name = "Branch 1"
            });

        // e.HasData(
        //     Branch.Seed(
        //         Guid.Parse("Z2EC68AA-E1F0-4699-B4A9-2E26C91B9E95"),
        //         "Branch 1")
        //    
        // );
    }
}