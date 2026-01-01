---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// API (Backend) - Program.cs
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

// CORS for Blazor - DEVELOPMENT ONLY!
// WARNING: AllowAnyOrigin() is insecure for production!
if (app.Environment.IsDevelopment())
{
    app.UseCors(policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
}
else
{
    // PRODUCTION: Restrict to specific origins
    app.UseCors(policy => policy
        .WithOrigins("https://yourapp.com", "https://www.yourapp.com")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());  // For cookies/auth headers
}

app.MapGet("/api/products", async (AppDbContext db) =>
    await db.Products.ToListAsync());

app.MapGet("/api/products/{id}", async (int id, AppDbContext db) =>
    await db.Products.FindAsync(id));

app.Run();

// BLAZOR (Frontend) - Using HttpClient
@inject HttpClient Http

<h3>Products</h3>

@if (products == null)
{
    <p>Loading...</p>
}
else
{
    <ul>
        @foreach (var product in products)
        {
            <li>@product.Name - $@product.Price</li>
        }
    </ul>
}

@code {
    private List<Product>? products;
    
    protected override async Task OnInitializedAsync()
    {
        // Call API!
        products = await Http.GetFromJsonAsync<List<Product>>(
            "https://localhost:5001/api/products");
    }
}

// POST REQUEST
private async Task CreateProduct(Product product)
{
    var response = await Http.PostAsJsonAsync(
        "https://localhost:5001/api/products", product);
    
    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("Product created!");
    }
}

// PUT REQUEST
private async Task UpdateProduct(int id, Product product)
{
    await Http.PutAsJsonAsync(
        $"https://localhost:5001/api/products/{id}", product);
}

// DELETE REQUEST
private async Task DeleteProduct(int id)
{
    await Http.DeleteAsync(
        $"https://localhost:5001/api/products/{id}");
}

// ERROR HANDLING
try
{
    products = await Http.GetFromJsonAsync<List<Product>>(
        "https://localhost:5001/api/products");
}
catch (HttpRequestException ex)
{
    errorMessage = $"API Error: {ex.Message}";
}
```
