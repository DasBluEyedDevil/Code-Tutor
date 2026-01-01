namespace ShopFlow.Application.Products.Queries;

/// <summary>
/// Query to get a single product by ID.
/// </summary>
public record GetProductByIdQuery(int Id);

/// <summary>
/// Query to get all products with optional filtering.
/// </summary>
public record GetProductsQuery(
    int? CategoryId = null,
    string? SearchTerm = null,
    bool IncludeInactive = false,
    int Page = 1,
    int PageSize = 20
);

/// <summary>
/// Query to search products by name.
/// </summary>
public record SearchProductsQuery(
    string SearchTerm,
    int Page = 1,
    int PageSize = 20
);
