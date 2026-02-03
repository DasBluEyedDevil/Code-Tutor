---
type: "KEY_POINT"
title: "Service Discovery in Aspire"
---

## Key Takeaways

- **Use service names as hostnames** -- `new Uri("http://catalog-api")` resolves automatically through Aspire's service discovery. No hardcoded ports or URLs needed.

- **Typed HttpClients keep API calls clean** -- `AddHttpClient<CatalogClient>()` registers a client class that receives a pre-configured HttpClient. The consuming code calls methods, not raw HTTP.

- **Service discovery only works with `AddServiceDefaults()`** -- without this registration in each service, the `http://service-name` URLs will not resolve. It is the foundation for inter-service communication.
