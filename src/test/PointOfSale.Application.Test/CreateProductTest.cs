using NSubstitute;
using PointOfSale.Application.Products;
using PointOfSale.Model.Products;
using PointOfSale.Model.Repositories;

namespace PointOfSale.Application.Test;
[TestFixture]
public class CreateProductTest
{
    private IUnitOfWork _unitOfWork;
    private CreateProduct _createProduct;
    [SetUp]
    public void Setup()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _createProduct = new CreateProduct(_unitOfWork);
    }

    [Test]
    public async Task Execute_WhenValidData_ShouldReturnSuccessAndCallRepository()
    {
        var createProductCommand = new CreateProductCommand("Product", 200, 100, 30, Guid.NewGuid(), false, 0, Guid.NewGuid(), Guid.NewGuid());

        var result = await _createProduct.Execute(createProductCommand);
        Assert.That(result.IsSuccess, Is.True);

       await _unitOfWork.Received(1).Products.AddAsync(Arg.Any<Product>());
    }
    [Test]
    public async Task Execute_WhenStockIsZero_ShouldReturnFailureAndNotCallRepository()
    {
        var command = new CreateProductCommand("Product", 200, 100, 0, Guid.NewGuid(), false, 0,  Guid.NewGuid(), Guid.NewGuid());
    
        var result = await _createProduct.Execute(command);
    
        Assert.That(!result.IsSuccess, Is.True);
        await _unitOfWork.Received(0).Products.AddAsync(Arg.Any<Product>());
    }


    [TearDown]
    public void TearDown()
    {
        _unitOfWork.Dispose();
    }
}