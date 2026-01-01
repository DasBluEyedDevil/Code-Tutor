using ShopFlow.Application.Products.Commands;
using ShopFlow.Application.Products.DTOs;
using ShopFlow.Application.Products.Handlers;
using ShopFlow.Application.Products.Queries;
using ShopFlow.Domain.Exceptions;

namespace ShopFlow.Api.Endpoints;

/// <summary>
/// Product API endpoints using minimal API pattern.
/// </summary>
public static class ProductEndpoints
{
    /// <summary>
    /// Maps all product-related endpoints.
    /// </summary>
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products")
            .WithTags("Products")
            .WithOpenApi();

        group.MapGet("/", GetAllProducts)
            .WithName("GetProducts")
            .WithDescription("Gets all products with optional filtering");

        group.MapGet("/{id:int}", GetProductById)
            .WithName("GetProductById")
            .WithDescription("Gets a single product by ID");

        group.MapPost("/", CreateProduct)
            .WithName("CreateProduct")
            .WithDescription("Creates a new product");

        group.MapPut("/{id:int}", UpdateProduct)
            .WithName("UpdateProduct")
            .WithDescription("Updates an existing product");

        group.MapDelete("/{id:int}", DeleteProduct)
            .WithName("DeleteProduct")
            .WithDescription("Permanently deletes a product");

        group.MapPost("/{id:int}/deactivate", DeactivateProduct)
            .WithName("DeactivateProduct")
            .WithDescription("Soft deletes (deactivates) a product");

        group.MapPost("/{id:int}/activate", ActivateProduct)
            .WithName("ActivateProduct")
            .WithDescription("Reactivates a deactivated product");

        group.MapPost("/{id:int}/stock", UpdateStock)
            .WithName("UpdateStock")
            .WithDescription("Updates product stock quantity");
    }

    /// <summary>
    /// Gets all products with optional filtering.
    /// </summary>
    private static async Task<IResult> GetAllProducts(
        ProductQueryHandler queryHandler,
        int? categoryId = null,
        string? search = null,
        bool includeInactive = false,
        int page = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var query = new GetProductsQuery(categoryId, search, includeInactive, page, pageSize);
        var products = await queryHandler.HandleAsync(query, cancellationToken);
        return Results.Ok(products);
    }

    /// <summary>
    /// Gets a single product by ID.
    /// </summary>
    private static async Task<IResult> GetProductById(
        int id,
        ProductQueryHandler queryHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetProductByIdQuery(id);
            var product = await queryHandler.HandleAsync(query, cancellationToken);
            return Results.Ok(product);
        }
        catch (EntityNotFoundException)
        {
            return Results.NotFound(new { Message = $"Product with id '{id}' was not found." });
        }
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    private static async Task<IResult> CreateProduct(
        CreateProductRequest request,
        ProductCommandHandler commandHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new CreateProductCommand(
                request.Name,
                request.Description,
                request.Price,
                request.Currency,
                request.CategoryId,
                request.StockQuantity
            );

            var product = await commandHandler.HandleAsync(command, cancellationToken);
            return Results.Created($"/api/products/{product.Id}", product);
        }
        catch (EntityNotFoundException ex)
        {
            return Results.BadRequest(new { Message = ex.Message });
        }
        catch (ValidationException ex)
        {
            return Results.BadRequest(new { Message = ex.Message, Errors = ex.Errors });
        }
    }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    private static async Task<IResult> UpdateProduct(
        int id,
        UpdateProductRequest request,
        ProductCommandHandler commandHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new UpdateProductCommand(
                id,
                request.Name,
                request.Description,
                request.Price,
                request.Currency,
                request.CategoryId
            );

            var product = await commandHandler.HandleAsync(command, cancellationToken);
            return Results.Ok(product);
        }
        catch (EntityNotFoundException)
        {
            return Results.NotFound(new { Message = $"Product with id '{id}' was not found." });
        }
        catch (ValidationException ex)
        {
            return Results.BadRequest(new { Message = ex.Message, Errors = ex.Errors });
        }
    }

    /// <summary>
    /// Permanently deletes a product.
    /// </summary>
    private static async Task<IResult> DeleteProduct(
        int id,
        ProductCommandHandler commandHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new DeleteProductCommand(id);
            await commandHandler.HandleAsync(command, cancellationToken);
            return Results.NoContent();
        }
        catch (EntityNotFoundException)
        {
            return Results.NotFound(new { Message = $"Product with id '{id}' was not found." });
        }
    }

    /// <summary>
    /// Soft deletes (deactivates) a product.
    /// </summary>
    private static async Task<IResult> DeactivateProduct(
        int id,
        ProductCommandHandler commandHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new DeactivateProductCommand(id);
            await commandHandler.HandleAsync(command, cancellationToken);
            return Results.NoContent();
        }
        catch (EntityNotFoundException)
        {
            return Results.NotFound(new { Message = $"Product with id '{id}' was not found." });
        }
    }

    /// <summary>
    /// Reactivates a deactivated product.
    /// </summary>
    private static async Task<IResult> ActivateProduct(
        int id,
        ProductCommandHandler commandHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new ActivateProductCommand(id);
            await commandHandler.HandleAsync(command, cancellationToken);
            return Results.NoContent();
        }
        catch (EntityNotFoundException)
        {
            return Results.NotFound(new { Message = $"Product with id '{id}' was not found." });
        }
    }

    /// <summary>
    /// Updates product stock quantity.
    /// </summary>
    private static async Task<IResult> UpdateStock(
        int id,
        UpdateStockRequest request,
        ProductCommandHandler commandHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new UpdateStockCommand(id, request.Quantity, request.IsAddition);
            await commandHandler.HandleAsync(command, cancellationToken);
            return Results.NoContent();
        }
        catch (EntityNotFoundException)
        {
            return Results.NotFound(new { Message = $"Product with id '{id}' was not found." });
        }
        catch (DomainException ex)
        {
            return Results.BadRequest(new { Message = ex.Message });
        }
    }
}

/// <summary>
/// Request model for creating a product.
/// </summary>
public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string Currency,
    int CategoryId,
    int StockQuantity = 0
);

/// <summary>
/// Request model for updating a product.
/// </summary>
public record UpdateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string Currency,
    int CategoryId
);

/// <summary>
/// Request model for updating stock.
/// </summary>
public record UpdateStockRequest(
    int Quantity,
    bool IsAddition
);
