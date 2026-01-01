using NSubstitute;
using ShopFlow.Application.Common.Interfaces;
using ShopFlow.Application.Products.Commands;
using ShopFlow.Application.Products.Handlers;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.Interfaces;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Tests.Unit.Application;

public class ProductCommandHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ProductCommandHandler _handler;

    public ProductCommandHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _categoryRepository = Substitute.For<ICategoryRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new ProductCommandHandler(_productRepository, _categoryRepository, _unitOfWork);
    }

    [Fact]
    public async Task HandleAsync_CreateProduct_WithValidData_ReturnsProductDto()
    {
        // Arrange
        var command = new CreateProductCommand(
            Name: "Test Product",
            Description: "Test Description",
            Price: 29.99m,
            Currency: "USD",
            CategoryId: 1,
            StockQuantity: 10
        );

        _categoryRepository.ExistsAsync(command.CategoryId, Arg.Any<CancellationToken>())
            .Returns(true);
        _productRepository.ExistsByNameAsync(command.Name, null, Arg.Any<CancellationToken>())
            .Returns(false);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Product", result.Name);
        Assert.Equal("Test Description", result.Description);
        Assert.Equal(29.99m, result.Price);
        Assert.Equal("USD", result.Currency);
        Assert.Equal(1, result.CategoryId);
        Assert.Equal(10, result.StockQuantity);
        Assert.True(result.IsActive);

        await _productRepository.Received(1).AddAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_CreateProduct_WithInvalidCategory_ThrowsEntityNotFoundException()
    {
        // Arrange
        var command = new CreateProductCommand(
            Name: "Test Product",
            Description: "Test Description",
            Price: 29.99m,
            Currency: "USD",
            CategoryId: 999,
            StockQuantity: 10
        );

        _categoryRepository.ExistsAsync(command.CategoryId, Arg.Any<CancellationToken>())
            .Returns(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(
            () => _handler.HandleAsync(command));

        Assert.Contains("Category", exception.Message);
        Assert.Contains("999", exception.Message);

        await _productRepository.DidNotReceive().AddAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_CreateProduct_WithDuplicateName_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateProductCommand(
            Name: "Existing Product",
            Description: "Test Description",
            Price: 29.99m,
            Currency: "USD",
            CategoryId: 1,
            StockQuantity: 10
        );

        _categoryRepository.ExistsAsync(command.CategoryId, Arg.Any<CancellationToken>())
            .Returns(true);
        _productRepository.ExistsByNameAsync(command.Name, null, Arg.Any<CancellationToken>())
            .Returns(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.HandleAsync(command));

        Assert.Contains("Existing Product", exception.Message);
        Assert.Contains("already exists", exception.Message);

        await _productRepository.DidNotReceive().AddAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_CreateProduct_WithZeroStock_ReturnsProductWithZeroStock()
    {
        // Arrange
        var command = new CreateProductCommand(
            Name: "Zero Stock Product",
            Description: "Product with no initial stock",
            Price: 19.99m,
            Currency: "USD",
            CategoryId: 1
        );

        _categoryRepository.ExistsAsync(command.CategoryId, Arg.Any<CancellationToken>())
            .Returns(true);
        _productRepository.ExistsByNameAsync(command.Name, null, Arg.Any<CancellationToken>())
            .Returns(false);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0, result.StockQuantity);
    }

    [Fact]
    public async Task HandleAsync_UpdateStock_WithAddition_IncreasesStock()
    {
        // Arrange
        var product = Product.Create("Test", "Description", Money.Create(10m, "USD"), 1, 5);
        var command = new UpdateStockCommand(ProductId: 1, Quantity: 10, IsAddition: true);

        _productRepository.GetByIdAsync(command.ProductId, Arg.Any<CancellationToken>())
            .Returns(product);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        Assert.Equal(15, product.StockQuantity);
        _productRepository.Received(1).Update(product);
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_UpdateStock_WithRemoval_DecreasesStock()
    {
        // Arrange
        var product = Product.Create("Test", "Description", Money.Create(10m, "USD"), 1, 10);
        var command = new UpdateStockCommand(ProductId: 1, Quantity: 5, IsAddition: false);

        _productRepository.GetByIdAsync(command.ProductId, Arg.Any<CancellationToken>())
            .Returns(product);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        Assert.Equal(5, product.StockQuantity);
        _productRepository.Received(1).Update(product);
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_UpdateStock_WithInsufficientStock_ThrowsDomainException()
    {
        // Arrange
        var product = Product.Create("Test", "Description", Money.Create(10m, "USD"), 1, 5);
        var command = new UpdateStockCommand(ProductId: 1, Quantity: 10, IsAddition: false);

        _productRepository.GetByIdAsync(command.ProductId, Arg.Any<CancellationToken>())
            .Returns(product);

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(() => _handler.HandleAsync(command));

        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_DeactivateProduct_SetsIsActiveToFalse()
    {
        // Arrange
        var product = Product.Create("Test", "Description", Money.Create(10m, "USD"), 1);
        var command = new DeactivateProductCommand(ProductId: 1);

        _productRepository.GetByIdAsync(command.ProductId, Arg.Any<CancellationToken>())
            .Returns(product);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        Assert.False(product.IsActive);
        _productRepository.Received(1).Update(product);
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_ActivateProduct_SetsIsActiveToTrue()
    {
        // Arrange
        var product = Product.Create("Test", "Description", Money.Create(10m, "USD"), 1);
        product.Deactivate();
        var command = new ActivateProductCommand(ProductId: 1);

        _productRepository.GetByIdAsync(command.ProductId, Arg.Any<CancellationToken>())
            .Returns(product);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        Assert.True(product.IsActive);
        _productRepository.Received(1).Update(product);
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_DeactivateProduct_WithNonExistentProduct_ThrowsEntityNotFoundException()
    {
        // Arrange
        var command = new DeactivateProductCommand(ProductId: 999);

        _productRepository.GetByIdAsync(command.ProductId, Arg.Any<CancellationToken>())
            .Returns((Product?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(
            () => _handler.HandleAsync(command));

        Assert.Contains("Product", exception.Message);
        Assert.Contains("999", exception.Message);
    }
}
