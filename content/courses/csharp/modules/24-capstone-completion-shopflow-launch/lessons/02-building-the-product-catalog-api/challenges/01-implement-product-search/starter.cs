// Application/Products/Queries/SearchProductsQuery.cs
using MediatR;
using ShopFlow.Application.Common;

namespace ShopFlow.Application.Products.Queries;

// TODO: Create SearchProductsQuery record implementing IRequest<PagedResult<ProductDto>>
// Include: SearchTerm, Category (optional), PageNumber (default 1), PageSize (default 20)


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
        // TODO: Implement search logic
        // 1. Start with base query (non-deleted products)
        // 2. Apply search term filter if provided (search Name and Description)
        // 3. Apply category filter if provided
        // 4. Get total count
        // 5. Apply pagination (Skip/Take)
        // 6. Project to ProductDto
        // 7. Return PagedResult
        
        throw new NotImplementedException();
    }
}