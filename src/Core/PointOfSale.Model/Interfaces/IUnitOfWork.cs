using PointOfSale.Model.Categories;
using PointOfSale.Model.Customers;
using PointOfSale.Model.Orders;
using PointOfSale.Model.Products;
using PointOfSale.Model.Sales;
using PointOfSale.Model.Suppliers;
using PointOfSale.Model.Users;

namespace PointOfSale.Model.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IGenericRepository<Customer, CustomerId> Customers { get; }
    IGenericRepository<Order, OrderId> Orders { get; }
    IGenericRepository<Product, ProductId> Products { get; }
    IGenericRepository<Category, CategoryId> Categories { get; }
    IGenericRepository<Branch, BranchId> Branches { get; }
    IGenericRepository<Supplier, SupplierId> Suppliers { get; }
    IGenericRepository<Sale, SaleId> Sales { get; }
    
    

    Task<int> SaveChangesAsync();
}