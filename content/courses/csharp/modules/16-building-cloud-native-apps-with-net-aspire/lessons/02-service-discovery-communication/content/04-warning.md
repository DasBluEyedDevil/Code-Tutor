---
type: "WARNING"
title: "Common Pitfalls"
---

## Service Discovery Gotchas

**AddServiceDefaults() is REQUIRED**: Without `builder.AddServiceDefaults()` in your service's Program.cs, service discovery URLs like `http://catalog-api` will NOT resolve. You'll get DNS errors!

**HTTPS vs HTTP**: Service discovery defaults to `http://` internally. For HTTPS, use `https+http://service-name` syntax, or configure endpoints explicitly in AppHost.

**Named Endpoints**: Some services expose multiple ports. Use `http://_endpointName.serviceName` syntax to target specific endpoints (e.g., `http://_grpc.catalog-api`).

**Container Networking**: Containers use resource names for internal DNS. If your container can't reach `postgres`, check the resource name matches exactly what you used in `AddPostgres("postgres")`.

**Don't Hardcode Ports**: Never use `http://localhost:5001` even locally! Service discovery handles ports automatically. Hardcoded URLs break when ports change or in production.

**HttpClient Lifecycle**: Never create `new HttpClient()` manually - it causes socket exhaustion. Always inject HttpClient through DI with `AddHttpClient<T>()`.