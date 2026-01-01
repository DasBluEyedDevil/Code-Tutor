using ShopFlow.Domain.Entities;

namespace ShopFlow.Domain.Interfaces;

/// <summary>
/// Repository interface for Order aggregate root.
/// Follows the Repository pattern from Domain-Driven Design.
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Gets an order by its unique identifier.
    /// </summary>
    /// <param name="id">The order ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The order if found; otherwise, null.</returns>
    Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all orders for a specific user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of orders for the user.</returns>
    Task<IReadOnlyList<Order>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new order to the repository.
    /// </summary>
    /// <param name="order">The order to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task AddAsync(Order order, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing order in the repository.
    /// </summary>
    /// <param name="order">The order to update.</param>
    void Update(Order order);

    /// <summary>
    /// Checks if an order exists with the given ID.
    /// </summary>
    /// <param name="id">The order ID to check.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if an order exists with the ID; otherwise, false.</returns>
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}
