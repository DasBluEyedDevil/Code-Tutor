namespace ShopFlow.Application.Products.Commands;

/// <summary>
/// Command to create a new product.
/// </summary>
public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    string Currency,
    int CategoryId,
    int StockQuantity = 0
);

/// <summary>
/// Command to update an existing product.
/// </summary>
public record UpdateProductCommand(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string Currency,
    int CategoryId
);

/// <summary>
/// Command to update product stock.
/// </summary>
public record UpdateStockCommand(
    int ProductId,
    int Quantity,
    bool IsAddition
);

/// <summary>
/// Command to deactivate a product (soft delete).
/// </summary>
public record DeactivateProductCommand(int ProductId);

/// <summary>
/// Command to reactivate a product.
/// </summary>
public record ActivateProductCommand(int ProductId);

/// <summary>
/// Command to permanently delete a product.
/// </summary>
public record DeleteProductCommand(int ProductId);
