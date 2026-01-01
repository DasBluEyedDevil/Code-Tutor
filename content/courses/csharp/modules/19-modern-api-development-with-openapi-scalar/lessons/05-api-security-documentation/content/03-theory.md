---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`SecuritySchemeType.Http`**: Standard HTTP authentication (Bearer, Basic). Most common for JWT tokens.

**`SecuritySchemeType.ApiKey`**: API key in header, query, or cookie. Good for server-to-server calls.

**`SecuritySchemeType.OAuth2`**: OAuth 2.0 flows (authorization code, client credentials). For third-party integrations.

**`BearerFormat = "JWT"`**: Hints that the bearer token is a JWT. Helps documentation tools and developers.

**`ParameterLocation.Header`**: Where the API key is sent. Options: Header, Query, Cookie.

**`document.Components.SecuritySchemes`**: Defines available auth methods. Referenced by name in security requirements.

**`document.SecurityRequirements`**: Global security applied to all endpoints. Individual endpoints can override.

**`.AllowAnonymous()`**: Explicitly marks endpoint as public. Overrides global security requirements.

**`.RequireAuthorization()`**: Endpoint requires authentication. User must present valid credentials.

**`.RequireAuthorization("PolicyName")`**: Requires specific authorization policy. For role-based or claims-based access.

**`AddDocumentTransformer`**: .NET 9 way to customize OpenAPI document. Add security schemes, info, servers, etc.