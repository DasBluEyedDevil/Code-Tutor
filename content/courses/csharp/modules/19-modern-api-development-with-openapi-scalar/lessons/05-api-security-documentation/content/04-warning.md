---
type: "WARNING"
title: "Common Pitfalls"
---

**Middleware Order Matters**: `app.UseAuthentication()` MUST come before `app.UseAuthorization()`, and both MUST come before `app.MapOpenApi()` and your endpoints. Wrong order = security doesn't work.

**AllowAnonymous is Explicit**: Without `.AllowAnonymous()`, public endpoints like health checks or login may require authentication if you set global security. Always be explicit about public endpoints.

**Document 401 AND 403**: Protected endpoints should declare both `.Produces(StatusCodes.Status401Unauthorized)` (not authenticated) and `.Produces(StatusCodes.Status403Forbidden)` (authenticated but not authorized). Different errors mean different things.

**Never Hardcode Secrets**: JWT signing keys, API keys, and credentials should NEVER be in source code. Use `IConfiguration`, environment variables, Azure Key Vault, or AWS Secrets Manager.

**Security Scheme Reference**: When referencing a security scheme in requirements, use `Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "SchemeName" }`. Missing this reference causes the scheme to not apply.

**Testing Protected Endpoints**: Scalar and Swagger UI have 'Authorize' buttons that let you enter tokens for testing. Configure `.WithPreferredScheme("Bearer")` in Scalar to hint which auth method to use.

**HTTPS in Production**: All security schemes assume HTTPS in production. Bearer tokens and API keys sent over HTTP are visible to attackers. Always use HTTPS.