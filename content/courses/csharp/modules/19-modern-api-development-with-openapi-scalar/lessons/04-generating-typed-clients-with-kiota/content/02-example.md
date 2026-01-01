---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ===== GENERATING TYPED CLIENTS WITH KIOTA =====

// Step 1: Install Kiota CLI
// dotnet tool install --global Microsoft.OpenApi.Kiota

// Step 2: Generate client from OpenAPI spec
// kiota generate -l CSharp -o ./Client -d https://api.example.com/openapi.json -c ApiClient -n MyApp.Client

// Step 3: Install required packages in your project
// dotnet add package Microsoft.Kiota.Abstractions
// dotnet add package Microsoft.Kiota.Http.HttpClientLibrary
// dotnet add package Microsoft.Kiota.Serialization.Json

// ===== USING THE GENERATED CLIENT =====

using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

// Create authentication provider (anonymous for public APIs)
var authProvider = new AnonymousAuthenticationProvider();

// Create HTTP client adapter
var adapter = new HttpClientRequestAdapter(authProvider)
{
    BaseUrl = "https://api.example.com"
};

// Create the typed API client
var client = new ApiClient(adapter);

// ===== STRONGLY TYPED API CALLS =====

// GET all products - fully typed!
var products = await client.Products.GetAsync();
foreach (var product in products ?? Enumerable.Empty<Product>())
{
    Console.WriteLine($"{product.Id}: {product.Name} - ${product.Price}");
}

// GET single product by ID
var laptop = await client.Products[1].GetAsync();
Console.WriteLine($"Found: {laptop?.Name}");

// POST create new product
var newProduct = await client.Products.PostAsync(new CreateProductRequest
{
    Name = "New Gadget",
    Price = 199.99m,
    Category = "Electronics"
});
Console.WriteLine($"Created: {newProduct?.Id}");

// PUT update product
await client.Products[1].PutAsync(new UpdateProductRequest
{
    Name = "Updated Laptop",
    Price = 1099.99m
});

// DELETE product
await client.Products[99].DeleteAsync();

// ===== QUERY PARAMETERS (Typed!) =====

// Search with typed query parameters
var searchResults = await client.Products.GetAsync(config =>
{
    config.QueryParameters.Category = "Electronics";
    config.QueryParameters.MinPrice = 100;
    config.QueryParameters.MaxPrice = 500;
});

// ===== ERROR HANDLING =====

try
{
    var product = await client.Products[99999].GetAsync();
}
catch (ApiException ex) when (ex.ResponseStatusCode == 404)
{
    Console.WriteLine("Product not found!");
}

// ===== KIOTA CLI COMMANDS =====

/*
// Generate client from local file
kiota generate -l CSharp -o ./Client -d ./openapi.json -c ApiClient -n MyApp.Client

// Generate from URL
kiota generate -l CSharp -o ./Client -d https://api.example.com/openapi/v1.json -c ApiClient -n MyApp.Client

// Update existing client (incremental)
kiota update -o ./Client

// Generate for specific API paths only
kiota generate -l CSharp -o ./Client -d ./openapi.json -c ApiClient --include-path "/products/**"

// Languages: CSharp, TypeScript, Python, Go, Java, Ruby, PHP, Swift
*/

Console.WriteLine("Kiota generates:");
Console.WriteLine("- Strongly typed request/response models");
Console.WriteLine("- Fluent API client with IntelliSense");
Console.WriteLine("- Query parameter objects");
Console.WriteLine("- Proper error types");
```
