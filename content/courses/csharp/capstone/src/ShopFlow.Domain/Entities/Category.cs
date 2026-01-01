namespace ShopFlow.Domain.Entities;

/// <summary>
/// Represents a product category in the catalog.
/// </summary>
public class Category
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<Product> _products = [];
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    // EF Core requires a parameterless constructor
    private Category() { }

    /// <summary>
    /// Creates a new Category with the specified name.
    /// </summary>
    /// <param name="name">The category name (required, max 100 characters).</param>
    /// <param name="description">Optional description of the category.</param>
    /// <exception cref="ArgumentException">Thrown when name is invalid.</exception>
    public static Category Create(string name, string? description = null)
    {
        ValidateName(name);

        return new Category
        {
            Name = name.Trim(),
            Description = description?.Trim(),
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Updates the category name.
    /// </summary>
    public void UpdateName(string name)
    {
        ValidateName(name);
        Name = name.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the category description.
    /// </summary>
    public void UpdateDescription(string? description)
    {
        Description = description?.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds a product to this category.
    /// </summary>
    internal void AddProduct(Product product)
    {
        if (!_products.Contains(product))
        {
            _products.Add(product);
        }
    }

    /// <summary>
    /// Removes a product from this category.
    /// </summary>
    internal void RemoveProduct(Product product)
    {
        _products.Remove(product);
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name is required.", nameof(name));

        if (name.Length > 100)
            throw new ArgumentException("Category name cannot exceed 100 characters.", nameof(name));
    }
}
