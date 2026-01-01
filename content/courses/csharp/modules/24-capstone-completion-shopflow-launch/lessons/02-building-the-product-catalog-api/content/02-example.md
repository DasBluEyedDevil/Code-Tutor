---
type: "EXAMPLE"
title: "Product Catalog Implementation with TDD"
---

Let us implement the CreateProduct use case using Test-Driven Development. We start by writing a test that describes the behavior we want, then implement just enough code to make it pass.

```csharp
// ===== STEP 1: Write the Failing Test First =====
// Tests/Application/Products/CreateProductHandlerTests.cs

using FluentAssertions;
using Moq;
using ShopFlow.Application.Products.Commands;
using ShopFlow.Application.Products.Handlers;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Repositories;

namespace ShopFlow.Tests.Application.Products;

public class CreateProductHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateProductHandler _handler;

    public CreateProductHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        
        _handler = new CreateProductHandler(
            _productRepositoryMock.Object,
            _categoryRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_CreatesProductAndReturnsId()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            Name = "Wireless Mouse",
            Description = "Ergonomic wireless mouse with USB receiver",
            Price = 29.99m,
            StockQuantity = 150,
            CategoryId = 1,
            Sku = "WM-001"
        };

        var category = new Category { Id = 1, Name = "Electronics" };
        _categoryRepositoryMock
            .Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        _productRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .Callback<Product, CancellationToken>((p, _) => p.Id = 42);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(42);
        
        _productRepositoryMock.Verify(
            r => r.AddAsync(It.Is<Product>(p =>
                p.Name == "Wireless Mouse" &&
                p.Price.Amount == 29.99m &&
                p.StockQuantity == 150),
            It.IsAny<CancellationToken>()), Times.Once);
        
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_CategoryNotFound_ReturnsFailure()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            Name = "Test Product",
            Price = 10.00m,
            CategoryId = 999
        };

        _categoryRepositoryMock
            .Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Category?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("Category");
        _productRepositoryMock.Verify(
            r => r.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_DuplicateSku_ReturnsFailure()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            Name = "New Product",
            Price = 15.00m,
            CategoryId = 1,
            Sku = "EXISTING-SKU"
        };

        _categoryRepositoryMock
            .Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Category { Id = 1, Name = "Test" });

        _productRepositoryMock
            .Setup(r => r.ExistsBySkuAsync("EXISTING-SKU", It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("SKU");
    }
}

// ===== STEP 2: Implement the Command and Handler =====
// Application/Products/Commands/CreateProductCommand.cs

using MediatR;
using ShopFlow.Application.Common;

namespace ShopFlow.Application.Products.Commands;

public record CreateProductCommand : IRequest<Result<int>>
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required decimal Price { get; init; }
    public int StockQuantity { get; init; } = 0;
    public required int CategoryId { get; init; }
    public required string Sku { get; init; }
    public List<string> ImageUrls { get; init; } = new();
}

// Application/Products/Handlers/CreateProductHandler.cs

using MediatR;
using ShopFlow.Application.Common;
using ShopFlow.Application.Products.Commands;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Repositories;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Application.Products.Handlers;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Result<int>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductHandler(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(
        CreateProductCommand command, 
        CancellationToken cancellationToken)
    {
        // Validate category exists
        var category = await _categoryRepository.GetByIdAsync(
            command.CategoryId, cancellationToken);
        
        if (category is null)
        {
            return Result<int>.Failure($"Category with ID {command.CategoryId} not found");
        }

        // Validate SKU is unique
        if (await _productRepository.ExistsBySkuAsync(command.Sku, cancellationToken))
        {
            return Result<int>.Failure($"Product with SKU '{command.Sku}' already exists");
        }

        // Create the product using domain factory method
        var product = Product.Create(
            name: command.Name,
            description: command.Description,
            price: Money.FromDecimal(command.Price, "USD"),
            sku: Sku.Create(command.Sku),
            categoryId: command.CategoryId,
            stockQuantity: command.StockQuantity);

        // Add images if provided
        foreach (var imageUrl in command.ImageUrls)
        {
            product.AddImage(imageUrl);
        }

        await _productRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(product.Id);
    }
}

// ===== STEP 3: Add the API Endpoint =====
// WebApi/Controllers/ProductsController.cs

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopFlow.Application.Products.Commands;
using ShopFlow.Application.Products.Queries;

namespace ShopFlow.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating product with SKU: {Sku}", command.Sku);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            _logger.LogWarning("Failed to create product: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        _logger.LogInformation("Created product with ID: {ProductId}", result.Value);
        return CreatedAtAction(
            nameof(GetProduct), 
            new { id = result.Value }, 
            new { id = result.Value });
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
```
