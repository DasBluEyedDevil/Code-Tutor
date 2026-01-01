using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopFlow.Application.Auth.Handlers;
using ShopFlow.Application.Auth.Services;
using ShopFlow.Application.Carts.Handlers;
using ShopFlow.Application.Common.Interfaces;
using ShopFlow.Application.Orders.Handlers;
using ShopFlow.Application.Products.Handlers;
using ShopFlow.Domain.Interfaces;
using ShopFlow.Infrastructure.Auth;
using ShopFlow.Infrastructure.Persistence;
using ShopFlow.Infrastructure.Persistence.Repositories;

namespace ShopFlow.Infrastructure;

/// <summary>
/// Extension methods for configuring infrastructure services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds infrastructure services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="connectionString">The database connection string.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        string connectionString)
    {
        // Register DbContext with SQLite
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        // Register Unit of Work
        services.AddScoped<IUnitOfWork>(provider =>
            provider.GetRequiredService<AppDbContext>());

        // Register JWT settings
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        // Register auth services
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        // Register repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // Register handlers
        services.AddScoped<ProductCommandHandler>();
        services.AddScoped<ProductQueryHandler>();
        services.AddScoped<CartCommandHandler>();
        services.AddScoped<CartQueryHandler>();
        services.AddScoped<OrderCommandHandler>();
        services.AddScoped<OrderQueryHandler>();
        services.AddScoped<AuthHandler>();

        return services;
    }

    /// <summary>
    /// Adds infrastructure services to the dependency injection container (legacy overload).
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="connectionString">The database connection string.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        // Register DbContext with SQLite
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        // Register Unit of Work
        services.AddScoped<IUnitOfWork>(provider =>
            provider.GetRequiredService<AppDbContext>());

        // Register repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // Register handlers
        services.AddScoped<ProductCommandHandler>();
        services.AddScoped<ProductQueryHandler>();
        services.AddScoped<CartCommandHandler>();
        services.AddScoped<CartQueryHandler>();
        services.AddScoped<OrderCommandHandler>();
        services.AddScoped<OrderQueryHandler>();
        services.AddScoped<AuthHandler>();

        return services;
    }

    /// <summary>
    /// Initializes the database by applying any pending migrations.
    /// In development, uses EnsureCreated for simplicity.
    /// In production, use Migrate() to apply migrations safely.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="useMigrations">If true, applies migrations. If false, uses EnsureCreated.</param>
    public static void InitializeDatabase(IServiceProvider serviceProvider, bool useMigrations = false)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (useMigrations)
        {
            // Apply pending migrations (recommended for production)
            context.Database.Migrate();
        }
        else
        {
            // Create database if it doesn't exist (simple for development)
            context.Database.EnsureCreated();
        }
    }
}
