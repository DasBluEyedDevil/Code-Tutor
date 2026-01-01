---
type: "EXAMPLE"
title: "Implementing Roles in ShopFlow"
---

This example demonstrates a complete role-based authorization implementation for ShopFlow, including role constants, database seeding, registration assignment, endpoint protection, and JWT integration.

```csharp
// ===== SHOPFLOW ROLE CONSTANTS =====
// Define roles as constants to prevent typos and enable refactoring

namespace ShopFlow.Authorization;

public static class ShopFlowRoles
{
    public const string Admin = "Admin";
    public const string Seller = "Seller";
    public const string Customer = "Customer";
    
    // Role hierarchy for permission checks
    public static readonly string[] AllRoles = { Admin, Seller, Customer };
    public static readonly string[] StaffRoles = { Admin, Seller };
}

// ===== SEEDING ROLES IN DATABASE =====
// Ensure roles exist when the application starts

public static class RoleSeeder
{
    public static async Task SeedRolesAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();
        
        foreach (var roleName in ShopFlowRoles.AllRoles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole(roleName);
                var result = await roleManager.CreateAsync(role);
                
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Failed to create role '{roleName}': " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}

// In Program.cs:
var app = builder.Build();
await RoleSeeder.SeedRolesAsync(app.Services);

// ===== ASSIGNING ROLES DURING REGISTRATION =====
// New users get Customer role by default

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    
    public async Task<AuthResult> RegisterAsync(RegisterDto dto)
    {
        var user = new ApplicationUser
        {
            Email = dto.Email,
            UserName = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };
        
        var createResult = await _userManager.CreateAsync(user, dto.Password);
        if (!createResult.Succeeded)
            return AuthResult.Failed(createResult.Errors);
        
        // Assign default Customer role to new registrations
        var roleResult = await _userManager.AddToRoleAsync(
            user, ShopFlowRoles.Customer);
        
        if (!roleResult.Succeeded)
        {
            // Rollback user creation if role assignment fails
            await _userManager.DeleteAsync(user);
            return AuthResult.Failed(roleResult.Errors);
        }
        
        return AuthResult.Success(user);
    }
    
    // Admin-only: Promote user to Seller role
    public async Task<bool> PromoteToSellerAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;
        
        // Check if already a seller
        if (await _userManager.IsInRoleAsync(user, ShopFlowRoles.Seller))
            return true; // Already has role
        
        var result = await _userManager.AddToRoleAsync(
            user, ShopFlowRoles.Seller);
        return result.Succeeded;
    }
}

// ===== PROTECTING ENDPOINTS WITH ROLES =====
// Using [Authorize(Roles = "...")] attribute

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // Anyone can view products
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<ProductDto>>> GetProducts()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }
    
    // Only Sellers and Admins can create products
    [HttpPost]
    [Authorize(Roles = $"{ShopFlowRoles.Seller},{ShopFlowRoles.Admin}")]
    public async Task<ActionResult<ProductDto>> CreateProduct(
        CreateProductDto dto)
    {
        var sellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var product = await _productService.CreateAsync(dto, sellerId!);
        return CreatedAtAction(nameof(GetProduct), 
            new { id = product.Id }, product);
    }
    
    // Only Admins can delete products
    [HttpDelete("{id}")]
    [Authorize(Roles = ShopFlowRoles.Admin)]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }
}

[ApiController]
[Route("api/admin")]
[Authorize(Roles = ShopFlowRoles.Admin)] // Entire controller requires Admin
public class AdminController : ControllerBase
{
    [HttpGet("users")]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }
    
    [HttpPost("users/{userId}/promote")]
    public async Task<IActionResult> PromoteToSeller(string userId)
    {
        var success = await _authService.PromoteToSellerAsync(userId);
        return success ? Ok() : NotFound();
    }
}

// ===== CHECKING ROLES IN CODE =====
// When you need conditional logic based on role

public class OrderService : IOrderService
{
    public async Task<OrderDto> GetOrderAsync(int orderId, ClaimsPrincipal user)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new NotFoundException("Order", orderId);
        
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        var isAdmin = user.IsInRole(ShopFlowRoles.Admin);
        var isSeller = user.IsInRole(ShopFlowRoles.Seller);
        
        // Admins can see any order
        if (isAdmin)
            return _mapper.Map<OrderDto>(order);
        
        // Sellers can see orders containing their products
        if (isSeller && order.Items.Any(i => i.Product.SellerId == userId))
            return _mapper.Map<OrderDto>(order);
        
        // Customers can only see their own orders
        if (order.CustomerId == userId)
            return _mapper.Map<OrderDto>(order);
        
        throw new ForbiddenException("You cannot view this order");
    }
}

// ===== INCLUDING ROLES IN JWT =====
// Roles must be in the token for authorization to work

public class JwtTokenService : ITokenService
{
    public async Task<string> GenerateTokenAsync(ApplicationUser user)
    {
        // Get user's roles from the database
        var roles = await _userManager.GetRolesAsync(user);
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        // Add each role as a separate claim
        // This is CRITICAL - authorization checks read from these claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
        var credentials = new SigningCredentials(
            key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

// ===== CONFIGURING JWT TO READ ROLES =====
// Ensure the ClaimTypes.Role is mapped correctly

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
            // Map the role claim type correctly
            RoleClaimType = ClaimTypes.Role
        };
    });
```
