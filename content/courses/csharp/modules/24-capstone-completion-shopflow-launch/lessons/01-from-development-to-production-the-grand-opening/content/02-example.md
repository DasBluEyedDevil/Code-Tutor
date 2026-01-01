---
type: "EXAMPLE"
title: "ShopFlow Feature Checklist"
---

Before launching ShopFlow, we need to verify that all core features are complete and working together. This checklist references the components we built throughout the course.

```csharp
// ===== SHOPFLOW FEATURE VERIFICATION CHECKLIST =====

// PRODUCT CATALOG API (Modules 8-10)
// - GET /api/products - List all products with pagination
// - GET /api/products/{id} - Get single product details
// - POST /api/products - Create new product (Admin only)
// - PUT /api/products/{id} - Update product (Admin only)
// - DELETE /api/products/{id} - Soft delete product (Admin only)
// - GET /api/products/search?q={term} - Full-text search
// - GET /api/categories - List product categories

public class ProductsController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<ProductDto>>> GetProducts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? category = null)
    {
        var products = await _productService.GetPagedAsync(page, pageSize, category);
        return Ok(products);
    }
}

// SHOPPING CART (Modules 11-12)
// - GET /api/cart - Get current user's cart
// - POST /api/cart/items - Add item to cart
// - PUT /api/cart/items/{id} - Update item quantity
// - DELETE /api/cart/items/{id} - Remove item from cart
// - DELETE /api/cart - Clear entire cart

public class CartService : ICartService
{
    public async Task<Cart> AddItemAsync(int userId, int productId, int quantity)
    {
        var cart = await GetOrCreateCartAsync(userId);
        var product = await _productRepository.GetByIdAsync(productId);
        
        if (product.StockQuantity < quantity)
            throw new InsufficientStockException(productId, quantity);
        
        cart.AddItem(product, quantity);
        await _cartRepository.UpdateAsync(cart);
        return cart;
    }
}

// USER AUTHENTICATION (Modules 20-22)
// - POST /api/auth/register - New user registration
// - POST /api/auth/login - Email/password login
// - POST /api/auth/refresh - Refresh JWT token
// - GET /api/auth/external/{provider} - OAuth login (Google, GitHub)
// - POST /api/auth/logout - Invalidate refresh token

services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<ShopFlowDbContext>()
.AddDefaultTokenProviders();

// ORDER PROCESSING (Modules 13-15)
// - POST /api/orders - Create order from cart
// - GET /api/orders - List user's orders
// - GET /api/orders/{id} - Get order details
// - PUT /api/orders/{id}/status - Update order status (Admin)
// - POST /api/orders/{id}/cancel - Cancel order

public class OrderService : IOrderService
{
    public async Task<Order> CreateOrderAsync(int userId, CreateOrderDto dto)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var cart = await _cartService.GetCartAsync(userId);
            var order = Order.CreateFromCart(cart, dto.ShippingAddress);
            
            await _orderRepository.AddAsync(order);
            await _inventoryService.ReserveStockAsync(order.Items);
            await _cartService.ClearCartAsync(userId);
            
            await transaction.CommitAsync();
            await _eventBus.PublishAsync(new OrderCreatedEvent(order));
            
            return order;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}

// ADMIN DASHBOARD (Modules 16-17)
// - GET /api/admin/dashboard - Sales metrics and stats
// - GET /api/admin/orders - All orders with filtering
// - GET /api/admin/users - User management
// - GET /api/admin/inventory - Stock levels and alerts

[Authorize(Policy = "AdminOnly")]
public class AdminController : ControllerBase
{
    [HttpGet("dashboard")]
    public async Task<ActionResult<DashboardDto>> GetDashboard()
    {
        var stats = await _analyticsService.GetDashboardStatsAsync();
        return Ok(stats);
    }
}

// BLAZOR STOREFRONT (Modules 18-19)
// - Home page with featured products
// - Product listing with category filtering
// - Product detail page
// - Shopping cart component
// - Checkout flow
// - User account pages
// - Order history

@page "/products/{Id:int}"
@inject IProductService ProductService
@inject ICartService CartService

<div class="product-detail">
    <ProductImages Images="@product.Images" />
    <ProductInfo Product="@product" />
    <AddToCartButton ProductId="@product.Id" 
                     OnAddToCart="HandleAddToCart" />
</div>

@code {
    [Parameter] public int Id { get; set; }
    private ProductDto? product;
    
    protected override async Task OnParametersSetAsync()
    {
        product = await ProductService.GetByIdAsync(Id);
    }
}
```
