using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Domain.Entities;

/// <summary>
/// Represents a product in the catalog.
/// Encapsulates business rules for product management.
/// </summary>
public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Money Price { get; private set; } = null!;
    public int StockQuantity { get; private set; }
    public int CategoryId { get; private set; }
    public Category? Category { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; } = true;

    // EF Core requires a parameterless constructor
    private Product() { }

    /// <summary>
    /// Creates a new Product with the specified details.
    /// </summary>
    /// <param name="name">Product name (required, max 200 characters).</param>
    /// <param name="description">Product description (required).</param>
    /// <param name="price">Product price (must be positive).</param>
    /// <param name="categoryId">The category this product belongs to.</param>
    /// <param name="stockQuantity">Initial stock quantity (default 0).</param>
    public static Product Create(
        string name,
        string description,
        Money price,
        int categoryId,
        int stockQuantity = 0)
    {
        ValidateName(name);
        ValidateDescription(description);
        ValidatePrice(price);
        ValidateStockQuantity(stockQuantity);

        return new Product
        {
            Name = name.Trim(),
            Description = description.Trim(),
            Price = price,
            CategoryId = categoryId,
            StockQuantity = stockQuantity,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Updates the product name.
    /// </summary>
    public void UpdateName(string name)
    {
        ValidateName(name);
        Name = name.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the product description.
    /// </summary>
    public void UpdateDescription(string description)
    {
        ValidateDescription(description);
        Description = description.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the product price.
    /// </summary>
    public void UpdatePrice(Money price)
    {
        ValidatePrice(price);
        Price = price;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Changes the product's category.
    /// </summary>
    public void ChangeCategory(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("Category ID must be positive.", nameof(categoryId));

        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds stock to the product inventory.
    /// </summary>
    /// <param name="quantity">The quantity to add (must be positive).</param>
    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity to add must be positive.", nameof(quantity));

        StockQuantity += quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Removes stock from the product inventory.
    /// </summary>
    /// <param name="quantity">The quantity to remove (must be positive).</param>
    /// <exception cref="DomainException">Thrown when insufficient stock is available.</exception>
    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity to remove must be positive.", nameof(quantity));

        if (quantity > StockQuantity)
            throw new DomainException($"Insufficient stock. Available: {StockQuantity}, Requested: {quantity}");

        StockQuantity -= quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the product has sufficient stock for the requested quantity.
    /// </summary>
    public bool HasSufficientStock(int quantity) => StockQuantity >= quantity;

    /// <summary>
    /// Deactivates the product (soft delete).
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Reactivates a deactivated product.
    /// </summary>
    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.", nameof(name));

        if (name.Length > 200)
            throw new ArgumentException("Product name cannot exceed 200 characters.", nameof(name));
    }

    private static void ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Product description is required.", nameof(description));
    }

    private static void ValidatePrice(Money price)
    {
        if (price.Amount <= 0)
            throw new ArgumentException("Product price must be greater than zero.", nameof(price));
    }

    private static void ValidateStockQuantity(int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Stock quantity cannot be negative.", nameof(quantity));
    }
}
