using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// TODO: Configure Authentication
// - Default scheme: Cookies
// - Add Cookie authentication with:
//   - LoginPath = "/login"
//   - AccessDeniedPath = "/forbidden"
//   - ExpireTimeSpan = 4 hours
//   - HttpOnly = true
//   - SecurePolicy = Always
// - Add JWT Bearer authentication with:
//   - ValidateIssuer, ValidateAudience, ValidateLifetime = true
//   - ValidIssuer = "ShopFlow"
//   - ValidAudience = "ShopFlowAPI"

// TODO: Configure Authorization
// - Add policy "AdminOnly" requiring role "Admin"
// - Add policy "CanManageProducts" requiring claim "Permission" with value "Products.Manage"

var app = builder.Build();

// TODO: Add authentication and authorization middleware in correct order

// TODO: Create endpoints
// GET / - AllowAnonymous, returns "Welcome to ShopFlow!"
// GET /dashboard - RequireAuthorization, returns "Dashboard for {username}"
// DELETE /products/{id} - RequireAuthorization("AdminOnly"), returns "Deleted product {id}"

app.Run();