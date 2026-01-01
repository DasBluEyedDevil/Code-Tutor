using ShopFlow.Domain.Entities;

namespace ShopFlow.Domain.Interfaces;

/// <summary>
/// Repository interface for Cart aggregate root.
/// Follows the Repository pattern from Domain-Driven Design.
/// </summary>
public interface ICartRepository
{
    /// <summary>
    /// Gets a cart by the user's unique identifier.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The cart if found; otherwise, null.</returns>
    Task<Cart?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a cart by its unique identifier.
    /// </summary>
    /// <param name="id">The cart ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The cart if found; otherwise, null.</returns>
    Task<Cart?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new cart to the repository.
    /// </summary>
    /// <param name="cart">The cart to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task AddAsync(Cart cart, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing cart in the repository.
    /// </summary>
    /// <param name="cart">The cart to update.</param>
    void Update(Cart cart);

    /// <summary>
    /// Removes a cart from the repository.
    /// </summary>
    /// <param name="cart">The cart to remove.</param>
    void Remove(Cart cart);

    /// <summary>
    /// Checks if a cart exists for the given user.
    /// </summary>
    /// <param name="userId">The user ID to check.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if a cart exists for the user; otherwise, false.</returns>
    Task<bool> ExistsForUserAsync(string userId, CancellationToken cancellationToken = default);
}
