---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`builder.Services.AddSingleton<I, T>()`**: Registers service with DI. ONE instance created, shared by ALL requests. Use for stateless services. <Interface, Implementation> pattern.

**`builder.Services.AddScoped<I, T>()`**: NEW instance per HTTP request. Shared within single request. Use for database contexts. Disposed after request ends.

**`builder.Services.AddTransient<I, T>()`**: NEW instance EVERY TIME requested. Use for lightweight, stateless services. Most isolated but potentially more overhead.

**`Keyed Services (.NET 8+)`**: Multiple implementations of same interface! Register: 'AddKeyedSingleton<IService, ImplA>("keyA")'. Inject: '([FromKeyedServices("keyA")] IService service)'. Perfect for strategy pattern!

**`Injecting into handlers`**: Add service as parameter to handler: (IService service) => ... ASP.NET Core automatically provides it! Can mix with route/query params.