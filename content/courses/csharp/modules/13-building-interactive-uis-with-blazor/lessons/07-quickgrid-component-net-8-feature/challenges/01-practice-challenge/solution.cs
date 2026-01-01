Console.WriteLine(@"
// ProductInventory.razor
using Microsoft.AspNetCore.Components.QuickGrid;
using System;
using System.Collections.Generic;
using System.Linq;

<div class=""container"">
    <h3>📦 Product Inventory Manager</h3>
    
    <div class=""mb-3"">
        <input class=""form-control"" 
               @bind=""searchTerm"" 
               @bind:event=""oninput""
               placeholder=""🔍 Search products..."" />
    </div>
    
    <QuickGrid Items=""@FilteredProducts"" Class=""table table-hover"" Pagination=""@pagination"">
        <PropertyColumn Property=""@(p => p.Name)"" Title=""Product Name"" Sortable=""true"" />
        
        <PropertyColumn Property=""@(p => p.Category)"" Title=""Category"" Sortable=""true"" />
        
        <TemplateColumn Title=""Price"" Sortable=""true"" SortBy=""@(GridSort<Product>.ByAscending(p => p.Price))"">
            <span class=""@(context.Price > 500 ? \""text-danger fw-bold\"" : \""text-success\"")"">
                $@context.Price.ToString(\""F2\"")
            </span>
        </TemplateColumn>
        
        <TemplateColumn Title=""Stock Status"">
            @if (context.Stock == 0)
            {
                <span class=""badge bg-danger"">Out of Stock</span>
            }
            else if (context.Stock < 10)
            {
                <span class=""badge bg-warning"">Low Stock (@context.Stock)</span>
            }
            else
            {
                <span class=""badge bg-success"">In Stock (@context.Stock)</span>
            }
        </TemplateColumn>
        
        <TemplateColumn Title=""Available"">
            @if (context.IsAvailable)
            {
                <span class=""text-success"">✓</span>
            }
            else
            {
                <span class=""text-danger"">✗</span>
            }
        </TemplateColumn>
        
        <TemplateColumn Title=""Actions"">
            <button class=""btn btn-sm btn-primary"" @onclick=""() => EditProduct(context.Id)"">Edit</button>
            <button class=""btn btn-sm btn-danger"" @onclick=""() => DeleteProduct(context.Id)"">Delete</button>
        </TemplateColumn>
    </QuickGrid>
    
    <Paginator State=""@pagination"" />
    
    <div class=""mt-3"">
        <strong>Total Products: @products.Count</strong> | 
        <strong>Showing: @FilteredProducts.Count()</strong>
    </div>
</div>

@code {
    private class Product {
        public int Id { get; set; }
        public string Name { get; set; } = """";
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; } = """";
        public bool IsAvailable { get; set; }
    }
    
    private List<Product> products = new() {
        new Product { Id = 1, Name = ""Laptop"", Price = 999.99m, Stock = 5, Category = ""Electronics"", IsAvailable = true },
        new Product { Id = 2, Name = ""Mouse"", Price = 29.99m, Stock = 50, Category = ""Electronics"", IsAvailable = true },
        new Product { Id = 3, Name = ""Monitor"", Price = 399.99m, Stock = 0, Category = ""Electronics"", IsAvailable = false },
        new Product { Id = 4, Name = ""Keyboard"", Price = 79.99m, Stock = 8, Category = ""Electronics"", IsAvailable = true },
        new Product { Id = 5, Name = ""Desk"", Price = 299.99m, Stock = 15, Category = ""Furniture"", IsAvailable = true }
    };
    
    private string searchTerm = """";
    private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
    
    private IQueryable<Product> FilteredProducts =>
        products.AsQueryable()
                .Where(p => string.IsNullOrEmpty(searchTerm) || 
                           p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                           p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
    
    private void EditProduct(int id) {
        Console.WriteLine($""Edit product {id}"");
    }
    
    private void DeleteProduct(int id) {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product != null) {
            products.Remove(product);
            Console.WriteLine($""Deleted: {product.Name}"");
        }
    }
}
"");

Console.WriteLine(@"
=== QUICKGRID BENEFITS ===");
Console.WriteLine("✓ Built-in sorting (click column headers)");
Console.WriteLine("✓ Pagination with Paginator component");
Console.WriteLine("✓ Custom templates for complex columns");
Console.WriteLine("✓ Conditional styling (colors, badges)");
Console.WriteLine("✓ Action buttons (Edit, Delete)");
Console.WriteLine("✓ Responsive table layout");
Console.WriteLine("\n✓ Professional data grid with minimal code!");