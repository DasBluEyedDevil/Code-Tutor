using ShopFlow.Domain.Entities;

namespace ShopFlow.Domain.Interfaces;

/// <summary>
/// Repository interface for Category aggregate root.
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Gets a category by its unique identifier.
    /// </summary>
    /// <param name="id">The category ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The category if found; otherwise, null.</returns>
    Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a category with its products loaded.
    /// </summary>
    /// <param name="id">The category ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The category with products if found; otherwise, null.</returns>
    Task<Category?> GetByIdWithProductsAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all categories.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of all categories.</returns>
    Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new category to the repository.
    /// </summary>
    /// <param name="category">The category to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task AddAsync(Category category, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing category in the repository.
    /// </summary>
    /// <param name="category">The category to update.</param>
    void Update(Category category);

    /// <summary>
    /// Removes a category from the repository.
    /// </summary>
    /// <param name="category">The category to remove.</param>
    void Remove(Category category);

    /// <summary>
    /// Checks if a category with the given name exists.
    /// </summary>
    /// <param name="name">The category name to check.</param>
    /// <param name="excludeId">Optional category ID to exclude from the check.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if a category with the name exists; otherwise, false.</returns>
    Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a category exists by ID.
    /// </summary>
    /// <param name="id">The category ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the category exists; otherwise, false.</returns>
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}
