using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

// TODO: Create ProductStatus enum with Draft, Published, Archived

// TODO: Create Product class with Id, Name, SellerId, Status

// TODO: Create ProductOperations static class with:
// - View, Edit, Delete, Publish as OperationAuthorizationRequirement

// TODO: Create ProductAuthorizationHandler
// - Extend AuthorizationHandler<OperationAuthorizationRequirement, Product>
// - Implement authorization logic:
//   - Admins can do everything
//   - View: Published products OR owner
//   - Edit: Only owner
//   - Delete: Only owner AND status is Draft
//   - Publish: Only owner AND status is Draft

// Program.cs:
// builder.Services.AddSingleton<IAuthorizationHandler, ProductAuthorizationHandler>();