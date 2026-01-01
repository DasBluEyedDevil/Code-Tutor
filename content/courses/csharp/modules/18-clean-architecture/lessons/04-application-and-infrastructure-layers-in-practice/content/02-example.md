---
type: "EXAMPLE"
title: "ShopFlow Application Layer"
---

The Application layer contains your use cases implemented as Commands (write operations) and Queries (read operations). Here's how ShopFlow implements the CQRS pattern with MediatR.

```csharp
// ===== SHOPFLOW APPLICATION LAYER =====
// Using CQRS pattern with MediatR

// ========== COMMANDS (Write Operations) ==========

// Command: Request object describing what to do
namespace ShopFlow.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    string Currency,
    int InitialStock,
    int CategoryId
) : IRequest<CreateProductResult>;

public record CreateProductResult(
    int ProductId,
    string Name,
    string PriceFormatted
);


// Handler: Implements the use case
namespace ShopFlow.Application.Products.Commands.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
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

    public async Task<CreateProductResult> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Validate business rules
        var categoryExists = await _categoryRepository.ExistsAsync(
            request.CategoryId, cancellationToken);
        
        if (!categoryExists)
            throw new NotFoundException("Category", request.CategoryId);

        // 2. Create domain entity using factory method
        var price = new Money(request.Price, request.Currency);
        
        var product = Product.Create(
            request.Name,
            request.Description,
            price,
            request.InitialStock);

        // 3. Persist using repository (interface - no EF knowledge here!)
        await _productRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 4. Return result DTO
        return new CreateProductResult(
            product.Id,
            product.Name,
            product.Price.ToString());
    }
}


// Validator: Input validation using FluentValidation
namespace ShopFlow.Application.Products.Commands.CreateProduct;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required")
            .Length(3).WithMessage("Currency must be 3-letter ISO code");

        RuleFor(x => x.InitialStock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Valid category is required");
    }
}


// ========== QUERIES (Read Operations) ==========

// Query: Request for data
namespace ShopFlow.Application.Products.Queries.GetProduct;

public record GetProductQuery(int ProductId) : IRequest<ProductDto?>;


// DTO: Data Transfer Object for responses
namespace ShopFlow.Application.Products.Queries.GetProduct;

public record ProductDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string Currency,
    string PriceFormatted,
    int StockQuantity,
    string Status,
    bool IsAvailable,
    DateTime CreatedAt,
    DateTime? LastModifiedAt
);


// Query Handler
namespace ShopFlow.Application.Products.Queries.GetProduct;

public class GetProductHandler : IRequestHandler<GetProductQuery, ProductDto?>
{
    private readonly IProductRepository _productRepository;

    public GetProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> Handle(
        GetProductQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(
            request.ProductId, cancellationToken);

        if (product is null)
            return null;

        // Map domain entity to DTO
        return new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price.Amount,
            product.Price.Currency,
            product.Price.ToString(),
            product.StockQuantity,
            product.Status.ToString(),
            product.IsAvailable,
            product.CreatedAt,
            product.LastModifiedAt);
    }
}


// ========== INTERFACES (Defined in Application) ==========

namespace ShopFlow.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

namespace ShopFlow.Application.Products;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}
```
