// Application/Products/Queries/SearchProductsQuery.cs
using MediatR;
using ShopFlow.Application.Common;

namespace ShopFlow.Application.Products.Queries;

public record SearchProductsQuery : IRequest<PagedResult<ProductDto>>
{
    public string? SearchTerm { get; init; }
    public string? Category { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}

// Application/Products/Handlers/SearchProductsHandler.cs
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopFlow.Application.Common;
using ShopFlow.Application.Products.Queries;
using ShopFlow.Infrastructure.Data;

namespace ShopFlow.Application.Products.Handlers;

public class SearchProductsHandler 
    : IRequestHandler<SearchProductsQuery, PagedResult<ProductDto>>
{
    private readonly ShopFlowDbContext _context;

    public SearchProductsHandler(ShopFlowDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<ProductDto>> Handle(
        SearchProductsQuery query, 
        CancellationToken cancellationToken)
    {
        // Start with base query - only active products
        var baseQuery = _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Where(p => !p.IsDeleted);

        // Apply search term filter if provided
        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var searchTerm = query.SearchTerm.ToLower();
            baseQuery = baseQuery.Where(p =>
                p.Name.ToLower().Contains(searchTerm) ||
                (p.Description != null && p.Description.ToLower().Contains(searchTerm)));
        }

        // Apply category filter if provided
        if (!string.IsNullOrWhiteSpace(query.Category))
        {
            baseQuery = baseQuery.Where(p => p.Category.Name == query.Category);
        }

        // Get total count for pagination metadata
        var totalCount = await baseQuery.CountAsync(cancellationToken);

        // Apply ordering and pagination, then project to DTO
        var items = await baseQuery
            .OrderBy(p => p.Name)
            .ThenBy(p => p.Id)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price.Amount,
                Currency = p.Price.Currency,
                Category = p.Category.Name,
                StockQuantity = p.StockQuantity,
                ImageUrl = p.Images.FirstOrDefault()
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<ProductDto>(
            items, 
            totalCount, 
            query.PageNumber, 
            query.PageSize);
    }
}