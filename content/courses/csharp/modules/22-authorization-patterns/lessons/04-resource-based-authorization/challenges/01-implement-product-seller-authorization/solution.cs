using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

public enum ProductStatus
{
    Draft,
    Published,
    Archived
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string SellerId { get; set; } = "";
    public ProductStatus Status { get; set; }
}

public static class ProductOperations
{
    public static readonly OperationAuthorizationRequirement View =
        new() { Name = nameof(View) };
    public static readonly OperationAuthorizationRequirement Edit =
        new() { Name = nameof(Edit) };
    public static readonly OperationAuthorizationRequirement Delete =
        new() { Name = nameof(Delete) };
    public static readonly OperationAuthorizationRequirement Publish =
        new() { Name = nameof(Publish) };
}

public class ProductAuthorizationHandler 
    : AuthorizationHandler<OperationAuthorizationRequirement, Product>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        Product product)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var isAdmin = context.User.IsInRole("Admin");
        var isOwner = product.SellerId == userId;
        
        if (isAdmin)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        
        switch (requirement.Name)
        {
            case nameof(ProductOperations.View):
                if (product.Status == ProductStatus.Published || isOwner)
                {
                    context.Succeed(requirement);
                }
                break;
                
            case nameof(ProductOperations.Edit):
                if (isOwner)
                {
                    context.Succeed(requirement);
                }
                break;
                
            case nameof(ProductOperations.Delete):
                if (isOwner && product.Status == ProductStatus.Draft)
                {
                    context.Succeed(requirement);
                }
                break;
                
            case nameof(ProductOperations.Publish):
                if (isOwner && product.Status == ProductStatus.Draft)
                {
                    context.Succeed(requirement);
                }
                break;
        }
        
        return Task.CompletedTask;
    }
}

// Program.cs:
// builder.Services.AddSingleton<IAuthorizationHandler, ProductAuthorizationHandler>();