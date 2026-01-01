---
type: "EXAMPLE"
title: "ShopFlow Order Authorization"
---

A complete implementation of resource-based authorization for ShopFlow orders, demonstrating ownership checks and role-based overrides.

```csharp
// ===== ORDER AUTHORIZATION REQUIREMENTS =====
// Define what operations can be performed on orders

public static class OrderOperations
{
    public static readonly OperationAuthorizationRequirement View =
        new() { Name = nameof(View) };
    public static readonly OperationAuthorizationRequirement Cancel =
        new() { Name = nameof(Cancel) };
    public static readonly OperationAuthorizationRequirement Refund =
        new() { Name = nameof(Refund) };
    public static readonly OperationAuthorizationRequirement UpdateStatus =
        new() { Name = nameof(UpdateStatus) };
}

// ===== ORDER AUTHORIZATION HANDLER =====
// Comprehensive handler for all order operations

public class OrderAuthorizationHandler 
    : AuthorizationHandler<OperationAuthorizationRequirement, Order>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        Order order)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var isAdmin = context.User.IsInRole(ShopFlowRoles.Admin);
        var isSeller = context.User.IsInRole(ShopFlowRoles.Seller);
        
        var isOrderOwner = order.CustomerId == userId;
        var isProductSeller = order.Items.Any(i => i.Product.SellerId == userId);
        
        switch (requirement.Name)
        {
            case nameof(OrderOperations.View):
                // Customers can view their own orders
                // Sellers can view orders containing their products
                // Admins can view all orders
                if (isOrderOwner || isProductSeller || isAdmin)
                {
                    context.Succeed(requirement);
                }
                break;
                
            case nameof(OrderOperations.Cancel):
                // Only owner can cancel, only if pending, within 24h
                // Admins can cancel any order in any state
                if (isAdmin)
                {
                    context.Succeed(requirement);
                }
                else if (isOrderOwner && CanBeCancelled(order))
                {
                    context.Succeed(requirement);
                }
                break;
                
            case nameof(OrderOperations.Refund):
                // Only admins and sellers can process refunds
                // Sellers can only refund for their own products
                if (isAdmin)
                {
                    context.Succeed(requirement);
                }
                else if (isSeller && isProductSeller && 
                         order.Status == OrderStatus.Delivered)
                {
                    context.Succeed(requirement);
                }
                break;
                
            case nameof(OrderOperations.UpdateStatus):
                // Only admins can update order status
                // Sellers can mark their items as shipped
                if (isAdmin)
                {
                    context.Succeed(requirement);
                }
                else if (isSeller && isProductSeller)
                {
                    // Sellers can only update to specific statuses
                    context.Succeed(requirement);
                }
                break;
        }
        
        return Task.CompletedTask;
    }
    
    private static bool CanBeCancelled(Order order)
    {
        // Business rules for cancellation
        if (order.Status != OrderStatus.Pending && 
            order.Status != OrderStatus.Processing)
            return false;
        
        var hoursSinceOrder = (DateTime.UtcNow - order.CreatedAt).TotalHours;
        return hoursSinceOrder <= 24;
    }
}

// ===== ORDERS CONTROLLER WITH RESOURCE AUTHORIZATION =====

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly IOrderService _orderService;
    
    public OrdersController(
        IOrderRepository orderRepository,
        IAuthorizationService authorizationService,
        IOrderService orderService)
    {
        _orderRepository = orderRepository;
        _authorizationService = authorizationService;
        _orderService = orderService;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrder(int id)
    {
        var order = await _orderRepository.GetByIdWithItemsAsync(id);
        if (order == null)
            return NotFound();
        
        var authResult = await _authorizationService.AuthorizeAsync(
            User, order, OrderOperations.View);
        
        if (!authResult.Succeeded)
            return Forbid();
        
        return Ok(_mapper.Map<OrderDto>(order));
    }
    
    [HttpPost("{id}/cancel")]
    public async Task<ActionResult> CancelOrder(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            return NotFound();
        
        var authResult = await _authorizationService.AuthorizeAsync(
            User, order, OrderOperations.Cancel);
        
        if (!authResult.Succeeded)
        {
            // Provide helpful error messages
            if (order.Status == OrderStatus.Shipped)
                return BadRequest("Cannot cancel shipped orders");
            if (order.Status == OrderStatus.Delivered)
                return BadRequest("Cannot cancel delivered orders");
            
            var hoursSinceOrder = (DateTime.UtcNow - order.CreatedAt).TotalHours;
            if (hoursSinceOrder > 24)
                return BadRequest("Cancellation window (24 hours) has expired");
            
            return Forbid();
        }
        
        await _orderService.CancelOrderAsync(order);
        return Ok(new { message = "Order cancelled successfully" });
    }
    
    [HttpPost("{id}/refund")]
    public async Task<ActionResult> RefundOrder(int id, RefundRequestDto dto)
    {
        var order = await _orderRepository.GetByIdWithItemsAsync(id);
        if (order == null)
            return NotFound();
        
        var authResult = await _authorizationService.AuthorizeAsync(
            User, order, OrderOperations.Refund);
        
        if (!authResult.Succeeded)
            return Forbid();
        
        await _orderService.ProcessRefundAsync(order, dto.Amount, dto.Reason);
        return Ok(new { message = "Refund processed successfully" });
    }
}

// ===== EXTENSION METHOD FOR CLEANER SYNTAX =====

public static class AuthorizationServiceExtensions
{
    public static async Task<bool> CanViewAsync<T>(
        this IAuthorizationService authService,
        ClaimsPrincipal user,
        T resource) where T : class
    {
        var result = await authService.AuthorizeAsync(
            user, resource, ResourceOperations.Read);
        return result.Succeeded;
    }
    
    public static async Task<bool> CanUpdateAsync<T>(
        this IAuthorizationService authService,
        ClaimsPrincipal user,
        T resource) where T : class
    {
        var result = await authService.AuthorizeAsync(
            user, resource, ResourceOperations.Update);
        return result.Succeeded;
    }
    
    public static async Task<bool> CanDeleteAsync<T>(
        this IAuthorizationService authService,
        ClaimsPrincipal user,
        T resource) where T : class
    {
        var result = await authService.AuthorizeAsync(
            user, resource, ResourceOperations.Delete);
        return result.Succeeded;
    }
}

// Usage in controller:
// if (!await _authorizationService.CanUpdateAsync(User, product))
//     return Forbid();
```
