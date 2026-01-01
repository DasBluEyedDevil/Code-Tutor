---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// FULL CRUD API
app.MapGet("/api/products", async (AppDbContext db) =>
    await db.Products.ToListAsync());

app.MapGet("/api/products/{id}", async (int id, AppDbContext db) =>
    await db.Products.FindAsync(id));

app.MapPost("/api/products", async (Product product, AppDbContext db) => {
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/api/products/{product.Id}", product);
});

app.MapPut("/api/products/{id}", async (int id, Product updated, AppDbContext db) => {
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();
    
    product.Name = updated.Name;
    product.Price = updated.Price;
    await db.SaveChangesAsync();
    return Results.Ok(product);
});

app.MapDelete("/api/products/{id}", async (int id, AppDbContext db) => {
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();
    
    db.Products.Remove(product);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// BLAZOR CRUD COMPONENT
@inject HttpClient Http

<h3>Product Manager</h3>

<button @onclick="ShowCreateForm">Add Product</button>

@if (showForm)
{
    <EditForm Model="@currentProduct" OnValidSubmit="SaveProduct">
        <InputText @bind-Value="currentProduct.Name" />
        <InputNumber @bind-Value="currentProduct.Price" />
        <button type="submit">Save</button>
        <button type="button" @onclick="CancelForm">Cancel</button>
    </EditForm>
}

<table>
    @foreach (var product in products)
    {
        <tr>
            <td>@product.Name</td>
            <td>$@product.Price</td>
            <td>
                <button @onclick="() => EditProduct(product)">Edit</button>
                <button @onclick="() => DeleteProduct(product.Id)">Delete</button>
            </td>
        </tr>
    }
</table>

@code {
    private List<Product> products = new();
    private Product currentProduct = new();
    private bool showForm = false;
    private bool isEditing = false;
    
    protected override async Task OnInitializedAsync() {
        await LoadProducts();
    }
    
    private async Task LoadProducts() {
        products = await Http.GetFromJsonAsync<List<Product>>(
            "https://localhost:5001/api/products") ?? new();
    }
    
    private void ShowCreateForm() {
        currentProduct = new Product();
        isEditing = false;
        showForm = true;
    }
    
    private void EditProduct(Product product) {
        currentProduct = new Product {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
        isEditing = true;
        showForm = true;
    }
    
    private async Task SaveProduct() {
        if (isEditing) {
            await Http.PutAsJsonAsync(
                $"https://localhost:5001/api/products/{currentProduct.Id}", 
                currentProduct);
        } else {
            await Http.PostAsJsonAsync(
                "https://localhost:5001/api/products", 
                currentProduct);
        }
        
        showForm = false;
        await LoadProducts();
    }
    
    private async Task DeleteProduct(int id) {
        await Http.DeleteAsync($"https://localhost:5001/api/products/{id}");
        await LoadProducts();
    }
    
    private void CancelForm() {
        showForm = false;
    }
}
```
