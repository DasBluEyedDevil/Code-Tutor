---
type: "THEORY"
title: "Pagination and Filtering"
---

Real product catalogs contain thousands or millions of items. Loading all products at once would overwhelm both the server and client. Pagination divides the data into manageable chunks, while filtering narrows results to what users actually want. Implementing these correctly requires understanding both the API design and database optimization.

## PagedResult Pattern

The PagedResult class encapsulates not just the data but metadata about the total available items and navigation. This allows the client to build proper pagination UI without additional API calls.

```csharp
public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public PagedResult(IReadOnlyList<T> items, int count, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = count;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
```

## Efficient LINQ Pagination

The key to efficient pagination is ensuring the database does the work, not the application. Skip and Take must be applied before materialization to generate proper SQL OFFSET/FETCH clauses.

```csharp
public async Task<PagedResult<ProductDto>> GetPagedAsync(
    int pageNumber, 
    int pageSize,
    string? category = null,
    string? searchTerm = null,
    CancellationToken cancellationToken = default)
{
    // Start with queryable - nothing executed yet
    var query = _context.Products
        .AsNoTracking()
        .Where(p => !p.IsDeleted);

    // Apply filters before pagination
    if (!string.IsNullOrWhiteSpace(category))
    {
        query = query.Where(p => p.Category.Name == category);
    }

    if (!string.IsNullOrWhiteSpace(searchTerm))
    {
        var term = searchTerm.ToLower();
        query = query.Where(p => 
            p.Name.ToLower().Contains(term) ||
            p.Description.ToLower().Contains(term));
    }

    // Get total count for pagination metadata
    var totalCount = await query.CountAsync(cancellationToken);

    // Apply ordering, skip, and take - then materialize
    var items = await query
        .OrderBy(p => p.Name)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price.Amount,
            ImageUrl = p.Images.FirstOrDefault()
        })
        .ToListAsync(cancellationToken);

    return new PagedResult<ProductDto>(items, totalCount, pageNumber, pageSize);
}
```

## Keyset Pagination for Large Datasets

Offset-based pagination becomes slow with large datasets because the database must scan all skipped rows. Keyset pagination, also called cursor-based pagination, uses the last item's identifier to fetch the next page directly.

```csharp
public async Task<KeysetPagedResult<ProductDto>> GetProductsAfterAsync(
    int? afterId,
    int pageSize,
    CancellationToken cancellationToken)
{
    var query = _context.Products
        .AsNoTracking()
        .Where(p => !p.IsDeleted)
        .OrderBy(p => p.Id);

    // If we have a cursor, start after that item
    if (afterId.HasValue)
    {
        query = query.Where(p => p.Id > afterId.Value);
    }

    // Take one extra to determine if there are more pages
    var items = await query
        .Take(pageSize + 1)
        .Select(p => new ProductDto { Id = p.Id, Name = p.Name })
        .ToListAsync(cancellationToken);

    var hasNextPage = items.Count > pageSize;
    if (hasNextPage)
    {
        items = items.Take(pageSize).ToList();
    }

    return new KeysetPagedResult<ProductDto>
    {
        Items = items,
        HasNextPage = hasNextPage,
        NextCursor = items.LastOrDefault()?.Id
    };
}
```

Keyset pagination generates efficient queries that use indexes directly, making page 1000 as fast as page 1. The tradeoff is that you cannot jump to arbitrary pages, only navigate forward and backward.