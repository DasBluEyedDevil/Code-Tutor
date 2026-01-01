using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// TODO: Add authentication and authorization services

// TODO: Configure OpenAPI with security schemes
// Use AddDocumentTransformer to add:
// - Bearer scheme (JWT)
// - ApiKey scheme (X-API-Key header)

var app = builder.Build();

// TODO: Add authentication/authorization middleware

app.MapOpenApi();

// ===== PUBLIC ENDPOINTS =====

// TODO: GET /health - AllowAnonymous

// TODO: POST /auth/login - AllowAnonymous, returns token

// ===== PROTECTED ENDPOINTS =====

// TODO: GET /orders - RequireAuthorization
// Returns user's orders

// TODO: POST /orders - RequireAuthorization
// Creates new order

// ===== ADMIN ENDPOINTS =====

// TODO: GET /admin/orders - RequireAuthorization("Admin")
// Returns ALL orders (admin only)

// TODO: DELETE /admin/orders/{id} - RequireAuthorization("Admin")
// Deletes order (admin only)

// TODO: Print security summary

app.Run();

// TODO: Define models (LoginRequest, Order, CreateOrderRequest)