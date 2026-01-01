---
type: "WARNING"
title: "Best Practices"
---

## HttpClient Best Practices

**Use IHttpClientFactory** (recommended for production):
- Prevents socket exhaustion issues
- Handles DNS changes automatically
- Enables retry policies with Polly
- Register in Program.cs: `builder.Services.AddHttpClient<MyApiClient>()`

**Typed Clients pattern**:
```csharp
public class ProductApiClient
{
    private readonly HttpClient _http;
    public ProductApiClient(HttpClient http) => _http = http;
    public Task<List<Product>> GetProductsAsync() => 
        _http.GetFromJsonAsync<List<Product>>("/api/products");
}
```

**Common Mistakes**:
- Creating `new HttpClient()` manually (causes socket exhaustion)
- Not handling HttpRequestException
- Forgetting to await async methods
- Missing CORS on API for WebAssembly apps