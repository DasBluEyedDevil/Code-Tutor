---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`@inject HttpClient Http`**: Injects HttpClient into component. Use 'Http' to make API calls. Configured in Program.cs with BaseAddress.

**`GetFromJsonAsync<T>(url)`**: GET request, deserializes JSON to T. Returns T or null. Throws HttpRequestException on failure. Use try/catch!

**`PostAsJsonAsync(url, data)`**: POST request, serializes data to JSON. Returns HttpResponseMessage. Check response.IsSuccessStatusCode for success.

**`CORS configuration`**: API must enable CORS for Blazor WebAssembly! Use UseCors() in development with AllowAnyOrigin(). **SECURITY WARNING**: AllowAnyOrigin() is unsafe for production - any website could call your API! In production, use WithOrigins() to restrict to your specific frontend domains.