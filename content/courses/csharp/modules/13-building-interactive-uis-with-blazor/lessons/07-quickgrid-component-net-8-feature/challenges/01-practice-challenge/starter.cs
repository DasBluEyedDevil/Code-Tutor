using Microsoft.AspNetCore.Components.QuickGrid;

// ProductInventory.razor
<div>
    <h3>📦 Product Inventory</h3>
    
    <input @bind="searchTerm" placeholder="Search products..." />
    
    <QuickGrid Items="@FilteredProducts">
        <PropertyColumn Property="@(p => p.Name)" Sortable="true" />
        <!-- Add other columns -->
    </QuickGrid>
    
    <p>Total Products: @products.Count</p>
</div>

@code {
    private class Product {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }
    }
    
    private List<Product> products = new();
    private string searchTerm = "";
}