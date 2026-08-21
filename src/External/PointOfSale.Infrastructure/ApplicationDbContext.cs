using Microsoft.EntityFrameworkCore;
using PointOfSale.Model.Categories;
using PointOfSale.Model.Customers;
using PointOfSale.Model.Orders;
using PointOfSale.Model.Products;
using PointOfSale.Model.Sales;
using PointOfSale.Model.Suppliers;
using PointOfSale.Model.Users;

namespace PointOfSale.Infrastructure;

public class ApplicationDbContext : DbContext
{
 
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :  base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }


    public DbSet<Branch>  Branches { get; set; }
    public DbSet<Category>  Categories { get; set; }
    public DbSet<Customer>   Customers { get; set; }
    public DbSet<Order>   Orders { get; set; }
   public DbSet<OrderDetail>  OrderDetails { get; set; }
    public DbSet<Product>  Products { get; set; }
   public DbSet<Sale>   Sales { get; set; }
    public DbSet<SaleDetail> SaleDetails { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<User>  Users { get; set; }
}