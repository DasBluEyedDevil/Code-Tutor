using NSubstitute;
using ShopFlow.Application.Products.Handlers;
using ShopFlow.Application.Products.Queries;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.Interfaces;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Tests.Unit.Application;

public class ProductQueryHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly ProductQueryHandler _handler;

    public ProductQueryHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new ProductQueryHandler(_productRepository);
    }

    [Fact]
    public async Task HandleAsync_GetProductById_WithValidId_ReturnsProductDto()
    {
        // Arrange
        var product = Product.Create("Test Product", "Test Description", Money.Create(29.99m, "USD"), 1, 10);
        var query = new GetProductByIdQuery(Id: 1);

        _productRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>())
            .Returns(product);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Product", result.Name);
        Assert.Equal("Test Description", result.Description);
        Assert.Equal(29.99m, result.Price);
        Assert.Equal("USD", result.Currency);
        Assert.Equal(1, result.CategoryId);
        Assert.Equal(10, result.StockQuantity);
        Assert.True(result.IsActive);

        await _productRepository.Received(1).GetByIdAsync(query.Id, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_GetProductById_WithInvalidId_ThrowsEntityNotFoundException()
    {
        // Arrange
        var query = new GetProductByIdQuery(Id: 999);

        _productRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>())
            .Returns((Product?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(
            () => _handler.HandleAsync(query));

        Assert.Contains("Product", exception.Message);
        Assert.Contains("999", exception.Message);
    }

    [Fact]
    public async Task HandleAsync_GetProducts_WithNoCriteria_ReturnsAllActiveProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            Product.Create("Product 1", "Description 1", Money.Create(10m, "USD"), 1),
            Product.Create("Product 2", "Description 2", Money.Create(20m, "USD"), 1),
            Product.Create("Product 3", "Description 3", Money.Create(30m, "USD"), 1)
        };
        var query = new GetProductsQuery();

        _productRepository.GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(products);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);

        await _productRepository.Received(1).GetAllAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_GetProducts_WithCategoryFilter_ReturnsProductsInCategory()
    {
        // Arrange
        var products = new List<Product>
        {
            Product.Create("Electronics 1", "Description 1", Money.Create(10m, "USD"), 2),
            Product.Create("Electronics 2", "Description 2", Money.Create(20m, "USD"), 2)
        };
        var query = new GetProductsQuery(CategoryId: 2);

        _productRepository.GetByCategoryAsync(2, Arg.Any<CancellationToken>())
            .Returns(products);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        await _productRepository.Received(1).GetByCategoryAsync(2, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_GetProducts_WithSearchTerm_ReturnsMatchingProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            Product.Create("Laptop Pro", "A powerful laptop", Money.Create(999m, "USD"), 1),
            Product.Create("Laptop Basic", "An affordable laptop", Money.Create(499m, "USD"), 1)
        };
        var query = new GetProductsQuery(SearchTerm: "Laptop");

        _productRepository.SearchByNameAsync("Laptop", Arg.Any<CancellationToken>())
            .Returns(products);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.All(result, p => Assert.Contains("Laptop", p.Name));

        await _productRepository.Received(1).SearchByNameAsync("Laptop", Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_GetProducts_ExcludesInactiveProducts_WhenIncludeInactiveIsFalse()
    {
        // Arrange
        var activeProduct = Product.Create("Active Product", "Description", Money.Create(10m, "USD"), 1);
        var inactiveProduct = Product.Create("Inactive Product", "Description", Money.Create(20m, "USD"), 1);
        inactiveProduct.Deactivate();

        var products = new List<Product> { activeProduct, inactiveProduct };
        var query = new GetProductsQuery(IncludeInactive: false);

        _productRepository.GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(products);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Active Product", result[0].Name);
    }

    [Fact]
    public async Task HandleAsync_GetProducts_IncludesInactiveProducts_WhenIncludeInactiveIsTrue()
    {
        // Arrange
        var activeProduct = Product.Create("Active Product", "Description", Money.Create(10m, "USD"), 1);
        var inactiveProduct = Product.Create("Inactive Product", "Description", Money.Create(20m, "USD"), 1);
        inactiveProduct.Deactivate();

        var products = new List<Product> { activeProduct, inactiveProduct };
        var query = new GetProductsQuery(IncludeInactive: true);

        _productRepository.GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(products);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task HandleAsync_GetProducts_AppliesPagination()
    {
        // Arrange
        var products = Enumerable.Range(1, 50)
            .Select(i => Product.Create($"Product {i}", $"Description {i}", Money.Create(i * 10m, "USD"), 1))
            .ToList();
        var query = new GetProductsQuery(Page: 2, PageSize: 10);

        _productRepository.GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(products);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Count);
        Assert.Equal("Product 11", result[0].Name);
        Assert.Equal("Product 20", result[9].Name);
    }

    [Fact]
    public async Task HandleAsync_SearchProducts_ReturnsProductSummaryDtos()
    {
        // Arrange
        var products = new List<Product>
        {
            Product.Create("Widget A", "Description A", Money.Create(15m, "USD"), 1, 5),
            Product.Create("Widget B", "Description B", Money.Create(25m, "USD"), 1, 0)
        };
        var query = new SearchProductsQuery(SearchTerm: "Widget");

        _productRepository.SearchByNameAsync("Widget", Arg.Any<CancellationToken>())
            .Returns(products);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Widget A", result[0].Name);
        Assert.True(result[0].IsInStock);
        Assert.Equal("Widget B", result[1].Name);
        Assert.False(result[1].IsInStock);
    }

    [Fact]
    public async Task HandleAsync_SearchProducts_ExcludesInactiveProducts()
    {
        // Arrange
        var activeProduct = Product.Create("Widget Active", "Description", Money.Create(15m, "USD"), 1, 5);
        var inactiveProduct = Product.Create("Widget Inactive", "Description", Money.Create(25m, "USD"), 1, 5);
        inactiveProduct.Deactivate();

        var products = new List<Product> { activeProduct, inactiveProduct };
        var query = new SearchProductsQuery(SearchTerm: "Widget");

        _productRepository.SearchByNameAsync("Widget", Arg.Any<CancellationToken>())
            .Returns(products);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Widget Active", result[0].Name);
    }

    [Fact]
    public async Task HandleAsync_SearchProducts_AppliesPagination()
    {
        // Arrange
        var products = Enumerable.Range(1, 30)
            .Select(i => Product.Create($"Widget {i}", $"Description {i}", Money.Create(i * 5m, "USD"), 1, i))
            .ToList();
        var query = new SearchProductsQuery(SearchTerm: "Widget", Page: 2, PageSize: 10);

        _productRepository.SearchByNameAsync("Widget", Arg.Any<CancellationToken>())
            .Returns(products);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Count);
        Assert.Equal("Widget 11", result[0].Name);
    }

    [Fact]
    public async Task HandleAsync_GetProducts_WithEmptyResult_ReturnsEmptyList()
    {
        // Arrange
        var query = new GetProductsQuery(CategoryId: 999);

        _productRepository.GetByCategoryAsync(999, Arg.Any<CancellationToken>())
            .Returns(new List<Product>());

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
