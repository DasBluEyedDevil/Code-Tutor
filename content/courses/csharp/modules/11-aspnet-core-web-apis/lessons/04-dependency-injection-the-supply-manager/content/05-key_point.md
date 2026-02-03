---
type: "KEY_POINT"
title: "DI Service Lifetimes"
---

## Key Takeaways

- **Three lifetimes: Singleton, Scoped, Transient** -- Singleton creates one instance shared by all requests. Scoped creates one per HTTP request. Transient creates a new instance every time. Choose based on the service's state requirements.

- **Register as interface, resolve automatically** -- `builder.Services.AddScoped<IProductService, ProductService>()` lets you swap implementations without changing endpoint code. This enables testing with mock services.

- **Keyed services (.NET 8+) support multiple implementations** -- `AddKeyedSingleton<ICache, RedisCache>("redis")` and inject with `[FromKeyedServices("redis")]`. Use for strategy patterns with multiple implementations of the same interface.
