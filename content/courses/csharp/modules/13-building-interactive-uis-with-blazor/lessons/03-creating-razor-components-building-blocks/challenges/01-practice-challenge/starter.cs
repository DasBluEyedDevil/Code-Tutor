// ProductCard.razor
<div class="card">
    <img src="@ImageUrl" alt="@Name" />
    <h4>@Name</h4>
    <p class="price">$@Price</p>
    
    @if (InStock)
    {
        <span class="badge-success">In Stock</span>
        <button @onclick="HandleAddToCart">Add to Cart</button>
    }
    else
    {
        <span class="badge-danger">Out of Stock</span>
    }
</div>

@code {
    [Parameter]
    public string Name { get; set; } = "";
    
    [Parameter]
    public decimal Price { get; set; }
    
    [Parameter]
    public string ImageUrl { get; set; } = "";
    
    [Parameter]
    public bool InStock { get; set; }
    
    [Parameter]
    public EventCallback OnAddToCart { get; set; }
    
    private async Task HandleAddToCart()
    {
        await OnAddToCart.InvokeAsync();
    }
}

// ProductList.razor
<h3>Our Products</h3>

<div class="product-grid">
    <ProductCard 
        Name="Laptop" 
        Price="999.99m" 
        ImageUrl="laptop.jpg"
        InStock="true"
        OnAddToCart="() => AddToCart(\"Laptop\")" />
    
    <!-- Add 2 more ProductCard components -->
</div>

<p>Cart Items: @cartCount</p>

@code {
    private int cartCount = 0;
    
    private void AddToCart(string productName)
    {
        cartCount++;
        // In real app: add to cart service
    }
}