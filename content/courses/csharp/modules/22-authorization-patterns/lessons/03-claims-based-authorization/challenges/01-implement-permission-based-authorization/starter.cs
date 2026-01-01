using Microsoft.AspNetCore.Authorization;

// TODO: Create Permissions static class with constants:
// - ProductsRead, ProductsEdit, ProductsDelete, OrdersRefund

// TODO: Create PermissionRequirement class
// - Implement IAuthorizationRequirement
// - Property: string Permission
// - Constructor accepting permission string

// TODO: Create PermissionHandler class
// - Extend AuthorizationHandler<PermissionRequirement>
// - Override HandleRequirementAsync
// - Check for permission claim OR Admin role

// TODO: Configure services (this goes in Program.cs)
// var builder = WebApplication.CreateBuilder(args);
// 
// Register PermissionHandler as IAuthorizationHandler
// 
// Configure AddAuthorization with policies:
// - CanEditProducts
// - CanDeleteProducts  
// - CanRefundOrders