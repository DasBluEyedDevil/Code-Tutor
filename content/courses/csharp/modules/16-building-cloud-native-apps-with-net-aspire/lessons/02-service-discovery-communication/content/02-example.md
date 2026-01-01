---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ===== AppHost/Program.cs =====
var builder = DistributedApplication.CreateBuilder(args);

// The API service
var catalogApi = builder.AddProject<Projects.CatalogApi>("catalog-api");

// Web app references the API (enables service discovery)
builder.AddProject<Projects.WebApp>("webapp")
    .WithReference(catalogApi);

builder.Build().Run();

// ===== WebApp/Program.cs =====
var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Register HttpClient for the catalog API
// Service discovery resolves "http://catalog-api" automatically!
builder.Services.AddHttpClient<CatalogApiClient>(client =>
{
    // Use service name, NOT hardcoded URL!
    client.BaseAddress = new Uri("http://catalog-api");
});

var app = builder.Build();
app.MapDefaultEndpoints();
app.Run();

// ===== WebApp/CatalogApiClient.cs =====
public class CatalogApiClient
{
    private readonly HttpClient _httpClient;
    
    public CatalogApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<Product>> GetProductsAsync()
    {
        // Just use relative path - base address is service-discovered!
        var response = await _httpClient.GetAsync("/api/products");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Product>>();
    }
    
    public async Task<Product?> GetProductAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Product>($"/api/products/{id}");
    }
}

public record Product(int Id, string Name, decimal Price);

// ===== ALTERNATIVE: Typed Client with Refit =====
// Install: dotnet add package Refit.HttpClientFactory

// Define API interface
public interface ICatalogApi
{
    [Get("/api/products")]
    Task<List<Product>> GetProductsAsync();
    
    [Get("/api/products/{id}")]
    Task<Product> GetProductAsync(int id);
    
    [Post("/api/products")]
    Task<Product> CreateProductAsync([Body] Product product);
}

// Register with Refit + service discovery
builder.Services
    .AddRefitClient<ICatalogApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://catalog-api"));

// ===== Using the client =====
public class ProductsController : Controller
{
    private readonly CatalogApiClient _catalogClient;
    
    public ProductsController(CatalogApiClient catalogClient)
    {
        _catalogClient = catalogClient;
    }
    
    public async Task<IActionResult> Index()
    {
        var products = await _catalogClient.GetProductsAsync();
        return View(products);
    }
}

Console.WriteLine("Service discovery configured!");
Console.WriteLine("http://catalog-api resolves automatically");
Console.WriteLine("No hardcoded URLs, works in any environment!");
```
