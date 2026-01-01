using ShopFlow.Application.Common.Interfaces;
using ShopFlow.Application.Products.Commands;
using ShopFlow.Application.Products.DTOs;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.Interfaces;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Application.Products.Handlers;

/// <summary>
/// Handles product-related commands.
/// </summary>
public class ProductCommandHandler
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductCommandHandler(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    public async Task<ProductDto> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken = default)
    {
        // Validate category exists
        if (!await _categoryRepository.ExistsAsync(command.CategoryId, cancellationToken))
        {
            throw new EntityNotFoundException("Category", command.CategoryId);
        }

        // Check for duplicate name
        if (await _productRepository.ExistsByNameAsync(command.Name, cancellationToken: cancellationToken))
        {
            throw new ValidationException($"A product with the name '{command.Name}' already exists.");
        }

        var price = Money.Create(command.Price, command.Currency);
        var product = Product.Create(
            command.Name,
            command.Description,
            price,
            command.CategoryId,
            command.StockQuantity
        );

        await _productRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(product);
    }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    public async Task<ProductDto> HandleAsync(UpdateProductCommand command, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new EntityNotFoundException("Product", command.Id);

        // Validate category exists
        if (!await _categoryRepository.ExistsAsync(command.CategoryId, cancellationToken))
        {
            throw new EntityNotFoundException("Category", command.CategoryId);
        }

        // Check for duplicate name (excluding current product)
        if (await _productRepository.ExistsByNameAsync(command.Name, command.Id, cancellationToken))
        {
            throw new ValidationException($"A product with the name '{command.Name}' already exists.");
        }

        product.UpdateName(command.Name);
        product.UpdateDescription(command.Description);
        product.UpdatePrice(Money.Create(command.Price, command.Currency));
        product.ChangeCategory(command.CategoryId);

        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(product);
    }

    /// <summary>
    /// Updates product stock.
    /// </summary>
    public async Task HandleAsync(UpdateStockCommand command, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken)
            ?? throw new EntityNotFoundException("Product", command.ProductId);

        if (command.IsAddition)
        {
            product.AddStock(command.Quantity);
        }
        else
        {
            product.RemoveStock(command.Quantity);
        }

        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Deactivates a product.
    /// </summary>
    public async Task HandleAsync(DeactivateProductCommand command, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken)
            ?? throw new EntityNotFoundException("Product", command.ProductId);

        product.Deactivate();
        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Activates a product.
    /// </summary>
    public async Task HandleAsync(ActivateProductCommand command, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken)
            ?? throw new EntityNotFoundException("Product", command.ProductId);

        product.Activate();
        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Permanently deletes a product.
    /// </summary>
    public async Task HandleAsync(DeleteProductCommand command, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken)
            ?? throw new EntityNotFoundException("Product", command.ProductId);

        _productRepository.Remove(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static ProductDto MapToDto(Product product) => new(
        product.Id,
        product.Name,
        product.Description,
        product.Price.Amount,
        product.Price.Currency,
        product.StockQuantity,
        product.CategoryId,
        product.Category?.Name,
        product.IsActive,
        product.CreatedAt,
        product.UpdatedAt
    );
}
