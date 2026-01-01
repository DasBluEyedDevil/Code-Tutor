---
type: "EXAMPLE"
title: "Authorization Handlers"
---

This example demonstrates implementing IAuthorizationHandler for resource-based authorization, including ownership checks and context-aware decisions.

```csharp
// ===== RESOURCE-BASED AUTHORIZATION PATTERN =====
// When authorization depends on the specific resource being accessed

// Step 1: Define an operation requirement
// Operations describe what the user wants to do with the resource

public static class ResourceOperations
{
    public static readonly OperationAuthorizationRequirement Read =
        new() { Name = nameof(Read) };
    public static readonly OperationAuthorizationRequirement Update =
        new() { Name = nameof(Update) };
    public static readonly OperationAuthorizationRequirement Delete =
        new() { Name = nameof(Delete) };
    public static readonly OperationAuthorizationRequirement Cancel =
        new() { Name = nameof(Cancel) };
}

// Step 2: Create a handler for the resource type
// This handler makes authorization decisions for Product resources

public class ProductAuthorizationHandler 
    : AuthorizationHandler<OperationAuthorizationRequirement, Product>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        Product resource)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var isAdmin = context.User.IsInRole("Admin");
        
        // Admins can do anything with any product
        if (isAdmin)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        
        // Handle each operation type
        switch (requirement.Name)
        {
            case nameof(ResourceOperations.Read):
                // Anyone can read published products
                // Only owner/admin can read draft products
                if (resource.Status == ProductStatus.Published ||
                    resource.SellerId == userId)
                {
                    context.Succeed(requirement);
                }
                break;
                
            case nameof(ResourceOperations.Update):
                // Only the seller who created the product can update it
                if (resource.SellerId == userId)
                {
                    context.Succeed(requirement);
                }
                break;
                
            case nameof(ResourceOperations.Delete):
                // Only owner can delete, and only if no orders reference it
                if (resource.SellerId == userId && !resource.HasOrders)
                {
                    context.Succeed(requirement);
                }
                break;
        }
        
        return Task.CompletedTask;
    }
}

// Step 3: Register the handler
// In Program.cs:
builder.Services.AddSingleton<IAuthorizationHandler, ProductAuthorizationHandler>();

// Step 4: Use IAuthorizationService in your controller or service

[ApiController]
[Route("api/products")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IAuthorizationService _authorizationService;
    
    public ProductsController(
        IProductRepository productRepository,
        IAuthorizationService authorizationService)
    {
        _productRepository = productRepository;
        _authorizationService = authorizationService;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
            return NotFound();
        
        // Check if the current user can read THIS specific product
        var authResult = await _authorizationService.AuthorizeAsync(
            User, product, ResourceOperations.Read);
        
        if (!authResult.Succeeded)
            return Forbid();
        
        return Ok(_mapper.Map<ProductDto>(product));
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(int id, UpdateProductDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
            return NotFound();
        
        // Check if user can update THIS specific product
        var authResult = await _authorizationService.AuthorizeAsync(
            User, product, ResourceOperations.Update);
        
        if (!authResult.Succeeded)
            return Forbid();
        
        // User is authorized - proceed with update
        _mapper.Map(dto, product);
        await _productRepository.UpdateAsync(product);
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
            return NotFound();
        
        var authResult = await _authorizationService.AuthorizeAsync(
            User, product, ResourceOperations.Delete);
        
        if (!authResult.Succeeded)
        {
            // Provide more context in the error
            if (product.HasOrders)
                return BadRequest("Cannot delete product with existing orders");
            return Forbid();
        }
        
        await _productRepository.DeleteAsync(product);
        return NoContent();
    }
}

// ===== COMBINING MULTIPLE REQUIREMENTS =====
// A handler can check multiple conditions

public class OrderCancellationHandler 
    : AuthorizationHandler<OperationAuthorizationRequirement, Order>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        Order order)
    {
        if (requirement.Name != nameof(ResourceOperations.Cancel))
            return Task.CompletedTask;
        
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var isAdmin = context.User.IsInRole("Admin");
        
        // Check ownership: only order owner or admin
        var isOwner = order.CustomerId == userId;
        if (!isOwner && !isAdmin)
            return Task.CompletedTask; // Not authorized
        
        // Check business rules: can only cancel pending orders
        if (order.Status != OrderStatus.Pending)
            return Task.CompletedTask; // Cannot cancel shipped/delivered orders
        
        // Check time limit: can only cancel within 24 hours
        var hoursSinceOrder = (DateTime.UtcNow - order.CreatedAt).TotalHours;
        if (hoursSinceOrder > 24 && !isAdmin)
            return Task.CompletedTask; // Cancellation window expired
        
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
```
