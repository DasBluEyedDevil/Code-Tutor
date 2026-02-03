---
type: "KEY_POINT"
title: "Connecting Blazor to an API"
---

## Key Takeaways

- **`@inject HttpClient Http` provides the HTTP client** -- configure `BaseAddress` in Program.cs. Use `GetFromJsonAsync<T>()` for GET and `PostAsJsonAsync()` for POST with automatic JSON serialization.

- **Always handle errors with try/catch** -- network calls fail. Wrap API calls in try/catch and show user-friendly error messages. Check `response.IsSuccessStatusCode` before reading the body.

- **Configure CORS on the API for Blazor WebAssembly** -- the browser blocks cross-origin requests by default. In production, use `WithOrigins()` to restrict access to your specific frontend domain, never `AllowAnyOrigin()`.
