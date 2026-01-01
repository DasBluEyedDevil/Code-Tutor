---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`WebApplication.CreateBuilder(args)`**: Creates the web application builder. Configures services, logging, configuration. This is the foundation of your app.

**`app.MapGet(route, handler)`**: Defines a GET endpoint. Route is URL pattern (\"/api/users\"). Handler is lambda that returns response. ASP.NET Core automatically converts to JSON!

**`Route parameters: {name}`**: Curly braces in route = parameter! /hello/{name} matches /hello/Bob. Parameter value passed to handler: (string name) => ...

**`TypedResults vs Results`**: TypedResults (recommended!) returns strongly-typed objects. Better for: compile-time checking, unit testing, automatic OpenAPI docs. Example: TypedResults.Ok(data) instead of Results.Ok(data).

**`app.Run()`**: Starts the web server! Listens for HTTP requests. Runs until stopped (Ctrl+C). This MUST be the last line!