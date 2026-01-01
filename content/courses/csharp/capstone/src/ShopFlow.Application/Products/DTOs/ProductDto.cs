namespace ShopFlow.Application.Products.DTOs;

/// <summary>
/// Data Transfer Object for Product entity.
/// </summary>
public record ProductDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string Currency,
    int StockQuantity,
    int CategoryId,
    string? CategoryName,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

/// <summary>
/// Simplified DTO for product listings.
/// </summary>
public record ProductSummaryDto(
    int Id,
    string Name,
    decimal Price,
    string Currency,
    int StockQuantity,
    bool IsInStock
);
