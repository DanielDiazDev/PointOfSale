using PointOfSale.Model.Categories;
using PointOfSale.Model.Customers;
using PointOfSale.Model.Orders;
using PointOfSale.Model.Products;
using PointOfSale.Model.Repositories;
using PointOfSale.Model.Sales;
using PointOfSale.Model.Suppliers;
using PointOfSale.Model.Users;

namespace PointOfSale.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IUserRepository Users { get; }
    public IGenericRepository<Customer, CustomerId> Customers { get; }
    public IGenericRepository<Order, OrderId> Orders { get; }
    public IGenericRepository<OrderDetail, OrderDetailId> OrderDetails { get; }
    public IGenericRepository<Product, ProductId> Products { get; }
    public IGenericRepository<Category, CategoryId> Categories { get; }
    public IGenericRepository<Branch, BranchId> Branches { get; }
    public IGenericRepository<Supplier, SupplierId> Suppliers { get; }
    public IGenericRepository<Sale, SaleId> Sales { get; }
    public IGenericRepository<SaleDetail, SaleDetailId> SaleDetails { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Users = new UserRepository(_context);
        Customers = new GenericRepository<Customer, CustomerId>(_context);
        Orders = new GenericRepository<Order, OrderId>(_context);
        Products = new GenericRepository<Product, ProductId>(_context);
        Categories = new GenericRepository<Category, CategoryId>(_context);
        Branches = new GenericRepository<Branch, BranchId>(_context);
        OrderDetails = new GenericRepository<OrderDetail, OrderDetailId>(_context);
        Suppliers  = new GenericRepository<Supplier, SupplierId>(_context);
        Sales    = new GenericRepository<Sale, SaleId>(_context);
        SaleDetails = new GenericRepository<SaleDetail, SaleDetailId>(_context);
    }

 

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}