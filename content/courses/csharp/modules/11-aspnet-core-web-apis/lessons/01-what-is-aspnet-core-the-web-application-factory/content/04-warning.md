---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Issues!

**Port conflicts**: Default ports 5000/5001 might be in use! Check error messages. Use 'builder.WebHost.UseUrls("http://localhost:5050")' to change.

**HTTPS in development**: Browser may block HTTP. Use 'dotnet dev-certs https --trust' to trust dev certificates.

**Results vs TypedResults**: Results.Ok() returns IResult (untyped). TypedResults.Ok() returns Ok<T> (typed). TypedResults gives better OpenAPI docs and compile-time safety - prefer it!

**Forgetting app.Run()**: Without it, server never starts! Must be LAST line after all MapGet/MapPost calls.

**JSON serialization issues**: Properties must be public with { get; set; } for JSON serialization. Private properties won't appear in response!