---
type: "EXAMPLE"
title: "Claims-Based Policies"
---

This example shows how to create sophisticated authorization policies using claims, including custom requirements and handlers for complex scenarios.

```csharp
// ===== SIMPLE CLAIM POLICIES =====
// Basic policies that require specific claims

builder.Services.AddAuthorization(options =>
{
    // Single claim requirement
    options.AddPolicy("RequireEmailVerified", policy =>
        policy.RequireClaim("email_verified", "true"));
    
    // Claim with multiple acceptable values
    options.AddPolicy("RequirePaidSubscription", policy =>
        policy.RequireClaim("account_tier", "premium", "enterprise", "team"));
    
    // Just require the claim exists (any value)
    options.AddPolicy("RequireDepartmentAssigned", policy =>
        policy.RequireClaim("department"));
    
    // Multiple claims (AND logic)
    options.AddPolicy("VerifiedPremiumInEngineering", policy =>
        policy.RequireClaim("email_verified", "true")
              .RequireClaim("account_tier", "premium")
              .RequireClaim("department", "Engineering"));
});

// ===== CUSTOM AUTHORIZATION REQUIREMENT =====
// For logic that RequireClaim cannot express

// Step 1: Define the requirement
public class MinimumProductLimitRequirement : IAuthorizationRequirement
{
    public int MinimumLimit { get; }
    
    public MinimumProductLimitRequirement(int minimumLimit)
    {
        MinimumLimit = minimumLimit;
    }
}

// Step 2: Create a handler for the requirement
public class MinimumProductLimitHandler 
    : AuthorizationHandler<MinimumProductLimitRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        MinimumProductLimitRequirement requirement)
    {
        var maxProductsClaim = context.User.FindFirst("max_products");
        
        if (maxProductsClaim == null)
        {
            // No limit claim means unlimited (or use default)
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        
        if (int.TryParse(maxProductsClaim.Value, out int maxProducts))
        {
            if (maxProducts >= requirement.MinimumLimit)
            {
                context.Succeed(requirement);
            }
            // If not sufficient, do NOT call Fail() - just don't succeed
            // This allows other handlers to potentially succeed
        }
        
        return Task.CompletedTask;
    }
}

// Step 3: Register handler and create policy
builder.Services.AddSingleton<IAuthorizationHandler, MinimumProductLimitHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanBulkUpload", policy =>
        policy.Requirements.Add(new MinimumProductLimitRequirement(100)));
});

// ===== PERMISSION-BASED AUTHORIZATION =====
// Pattern for granular feature permissions

public static class Permissions
{
    // Product permissions
    public const string ProductsRead = "Products.Read";
    public const string ProductsCreate = "Products.Create";
    public const string ProductsEdit = "Products.Edit";
    public const string ProductsDelete = "Products.Delete";
    
    // Order permissions
    public const string OrdersRead = "Orders.Read";
    public const string OrdersProcess = "Orders.Process";
    public const string OrdersRefund = "Orders.Refund";
    public const string OrdersCancel = "Orders.Cancel";
    
    // Admin permissions
    public const string UsersManage = "Users.Manage";
    public const string SettingsManage = "Settings.Manage";
    public const string ReportsView = "Reports.View";
}

// Custom requirement for permission checks
public class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }
    
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        // Check if user has the specific permission claim
        if (context.User.HasClaim("permission", requirement.Permission))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        
        // Admins have all permissions
        if (context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        
        // Check for wildcard permissions (Products.* grants all Product permissions)
        var permissionCategory = requirement.Permission.Split('.')[0];
        if (context.User.HasClaim("permission", $"{permissionCategory}.*"))
        {
            context.Succeed(requirement);
        }
        
        return Task.CompletedTask;
    }
}

// Register and configure
builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

builder.Services.AddAuthorization(options =>
{
    // Create policies for each permission
    options.AddPolicy("CanReadProducts", policy =>
        policy.Requirements.Add(new PermissionRequirement(Permissions.ProductsRead)));
    
    options.AddPolicy("CanEditProducts", policy =>
        policy.Requirements.Add(new PermissionRequirement(Permissions.ProductsEdit)));
    
    options.AddPolicy("CanDeleteProducts", policy =>
        policy.Requirements.Add(new PermissionRequirement(Permissions.ProductsDelete)));
    
    options.AddPolicy("CanProcessOrders", policy =>
        policy.Requirements.Add(new PermissionRequirement(Permissions.OrdersProcess)));
    
    options.AddPolicy("CanRefundOrders", policy =>
        policy.Requirements.Add(new PermissionRequirement(Permissions.OrdersRefund)));
});

// ===== USING PERMISSION POLICIES =====

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "CanReadProducts")]
    public async Task<ActionResult<List<ProductDto>>> GetProducts()
    {
        return Ok(await _productService.GetAllAsync());
    }
    
    [HttpPut("{id}")]
    [Authorize(Policy = "CanEditProducts")]
    public async Task<ActionResult> UpdateProduct(int id, UpdateProductDto dto)
    {
        await _productService.UpdateAsync(id, dto);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Policy = "CanDeleteProducts")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }
}

// ===== ASSIGNING PERMISSIONS TO USERS =====

public class PermissionService : IPermissionService
{
    private readonly UserManager<ApplicationUser> _userManager;
    
    // Default permissions by role
    private static readonly Dictionary<string, string[]> RolePermissions = new()
    {
        ["Admin"] = new[] { "*" },  // All permissions
        ["Seller"] = new[] 
        { 
            Permissions.ProductsRead,
            Permissions.ProductsCreate,
            Permissions.ProductsEdit,
            Permissions.OrdersRead
        },
        ["Customer"] = new[] 
        { 
            Permissions.ProductsRead,
            Permissions.OrdersRead
        }
    };
    
    public async Task AssignDefaultPermissionsAsync(ApplicationUser user, string role)
    {
        if (!RolePermissions.TryGetValue(role, out var permissions))
            return;
        
        var claims = permissions
            .Select(p => new Claim("permission", p))
            .ToList();
        
        await _userManager.AddClaimsAsync(user, claims);
    }
    
    public async Task GrantPermissionAsync(string userId, string permission)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return;
        
        var existingClaims = await _userManager.GetClaimsAsync(user);
        if (existingClaims.Any(c => c.Type == "permission" && c.Value == permission))
            return;
        
        await _userManager.AddClaimAsync(user, new Claim("permission", permission));
    }
    
    public async Task RevokePermissionAsync(string userId, string permission)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return;
        
        var claims = await _userManager.GetClaimsAsync(user);
        var claimToRemove = claims.FirstOrDefault(
            c => c.Type == "permission" && c.Value == permission);
        
        if (claimToRemove != null)
        {
            await _userManager.RemoveClaimAsync(user, claimToRemove);
        }
    }
}
```
