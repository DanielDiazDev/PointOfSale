using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PointOfSale.Application.Categories.Update;
using PointOfSale.Application.Products;
using PointOfSale.Model.Categories;
using PointOfSale.Model.Products;
using PointOfSale.Model.Repositories;
using PointOfSale.Model.Suppliers;
using PointOfSale.Shared;

namespace PointOfSale.API.Test;

[TestFixture]
public class ProductsModuleIntegrationTest
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        var unityOfWork = Substitute.For<IUnitOfWork>();
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(IUnitOfWork));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                        services.AddSingleton(unityOfWork);
                    }
                });
            });
        _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Test]
    public async Task GetProducts_WhenCalled_ShouldReturnOk()
    {
        var response = await _client.GetAsync("products");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    [Test]
    public async Task CreateProduct_WhenValidCommand_ShouldReturnSuccess()
    {
        var command = new CreateProductCommand(
            "ProductTest",
            200,
            100,
            30,
            Guid.NewGuid(),
            false,
            0,
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        var response = await _client.PostAsJsonAsync("products/create", command);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var result = await response.Content.ReadFromJsonAsync<Result>();
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public async Task GetProductById_WhenNotExists_ShouldReturnFailure()
    {
        var id = Guid.NewGuid();
        var response = await _client.GetAsync($"/products/{id}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var result = await response.Content.ReadFromJsonAsync<Result>();
        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public async Task DeleteProduct_WhenCalled_ShouldReturnSuccess()
    {
        var id = Guid.NewGuid();
        var response = await _client.DeleteAsync($"/products/{id}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task UpdateProduct_WhenValidCommand_ShouldReturnSuccess()
    {
        var command = new UpdateProductCommand(
            Guid.NewGuid(),
            "UpdatedProduct",
            300,
            150,
            20,
           Guid.NewGuid(),
           false, 
            0,
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        var response = await _client.PutAsJsonAsync("/products/", command);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}