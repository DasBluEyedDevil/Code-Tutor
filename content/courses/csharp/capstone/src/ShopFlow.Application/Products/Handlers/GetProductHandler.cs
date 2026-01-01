using ShopFlow.Application.Products.DTOs;
using ShopFlow.Application.Products.Queries;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.Interfaces;

namespace ShopFlow.Application.Products.Handlers;

/// <summary>
/// Handles product-related queries.
/// </summary>
public class ProductQueryHandler
{
    private readonly IProductRepository _productRepository;

    public ProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    /// <summary>
    /// Gets a single product by ID.
    /// </summary>
    public async Task<ProductDto> HandleAsync(GetProductByIdQuery query, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(query.Id, cancellationToken)
            ?? throw new EntityNotFoundException("Product", query.Id);

        return MapToDto(product);
    }

    /// <summary>
    /// Gets products with optional filtering.
    /// </summary>
    public async Task<IReadOnlyList<ProductDto>> HandleAsync(GetProductsQuery query, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Product> products;

        if (query.CategoryId.HasValue)
        {
            products = await _productRepository.GetByCategoryAsync(query.CategoryId.Value, cancellationToken);
        }
        else if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            products = await _productRepository.SearchByNameAsync(query.SearchTerm, cancellationToken);
        }
        else
        {
            products = await _productRepository.GetAllAsync(cancellationToken);
        }

        // Filter inactive if needed
        if (!query.IncludeInactive)
        {
            products = products.Where(p => p.IsActive).ToList();
        }

        // Apply pagination
        var pagedProducts = products
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return pagedProducts.Select(MapToDto).ToList();
    }

    /// <summary>
    /// Searches products by name.
    /// </summary>
    public async Task<IReadOnlyList<ProductSummaryDto>> HandleAsync(SearchProductsQuery query, CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.SearchByNameAsync(query.SearchTerm, cancellationToken);

        var pagedProducts = products
            .Where(p => p.IsActive)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return pagedProducts.Select(MapToSummaryDto).ToList();
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

    private static ProductSummaryDto MapToSummaryDto(Product product) => new(
        product.Id,
        product.Name,
        product.Price.Amount,
        product.Price.Currency,
        product.StockQuantity,
        product.StockQuantity > 0
    );
}
