using Microsoft.AspNetCore.Authorization;

public static class Permissions
{
    public const string ProductsRead = "Products.Read";
    public const string ProductsEdit = "Products.Edit";
    public const string ProductsDelete = "Products.Delete";
    public const string OrdersRefund = "Orders.Refund";
}

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
        if (context.User.HasClaim("permission", requirement.Permission))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        
        if (context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
        }
        
        return Task.CompletedTask;
    }
}

// Program.cs configuration:
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanEditProducts", policy =>
        policy.Requirements.Add(new PermissionRequirement(Permissions.ProductsEdit)));
    
    options.AddPolicy("CanDeleteProducts", policy =>
        policy.Requirements.Add(new PermissionRequirement(Permissions.ProductsDelete)));
    
    options.AddPolicy("CanRefundOrders", policy =>
        policy.Requirements.Add(new PermissionRequirement(Permissions.OrdersRefund)));
});