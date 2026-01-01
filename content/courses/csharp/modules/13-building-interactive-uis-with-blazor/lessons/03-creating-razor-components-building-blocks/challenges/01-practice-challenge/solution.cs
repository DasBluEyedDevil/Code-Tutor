using System;

Console.WriteLine(@"
=== PRODUCTCARD COMPONENT ===");
Console.WriteLine(@"
// ProductCard.razor
<div class=""card"">
    <img src=""@ImageUrl"" alt=""@Name"" style=""width:100%"" />
    <div class=""card-body"">
        <h4>@Name</h4>
        <p class=""price"" style=""font-size:1.5em"">$@Price.ToString(\""F2\"")</p>
        
        @if (InStock)
        {
            <span class=""badge bg-success"">✓ In Stock</span>
            <button class=""btn btn-primary"" @onclick=""HandleAddToCart"">Add to Cart</button>
        }
        else
        {
            <span class=""badge bg-danger"">✗ Out of Stock</span>
            <button class=""btn btn-secondary"" disabled>Unavailable</button>
        }
    </div>
</div>

@code {
    [Parameter]
    public string Name { get; set; } = """";
    
    [Parameter]
    public decimal Price { get; set; }
    
    [Parameter]
    public string ImageUrl { get; set; } = """";
    
    [Parameter]
    public bool InStock { get; set; }
    
    [Parameter]
    public EventCallback<string> OnAddToCart { get; set; }
    
    private async Task HandleAddToCart()
    {
        await OnAddToCart.InvokeAsync(Name);
    }
}
"");

Console.WriteLine(@"
=== PRODUCTLIST COMPONENT ===");
Console.WriteLine(@"
// ProductList.razor
<div class=""container"">
    <h3>Our Products</h3>
    
    <div class=""alert alert-info"">
        <strong>Cart Items: @cartCount</strong>
    </div>
    
    <div class=""row"">
        <div class=""col-md-4"">
            <ProductCard 
                Name=""Laptop"" 
                Price=""999.99m"" 
                ImageUrl=""laptop.jpg""
                InStock=""true""
                OnAddToCart=""AddToCart"" />
        </div>
        
        <div class=""col-md-4"">
            <ProductCard 
                Name=""Mouse"" 
                Price=""29.99m"" 
                ImageUrl=""mouse.jpg""
                InStock=""true""
                OnAddToCart=""AddToCart"" />
        </div>
        
        <div class=""col-md-4"">
            <ProductCard 
                Name=""Monitor"" 
                Price=""399.99m"" 
                ImageUrl=""monitor.jpg""
                InStock=""false""
                OnAddToCart=""AddToCart"" />
        </div>
    </div>
</div>

@code {
    private int cartCount = 0;
    private List<string> cartItems = new();
    
    private void AddToCart(string productName)
    {
        cartCount++;
        cartItems.Add(productName);
        Console.WriteLine($""Added {productName} to cart! Total: {cartCount}"");
    }
}
"");

Console.WriteLine(@"
=== KEY CONCEPTS ===");
Console.WriteLine("✓ [Parameter] - Makes property configurable from parent");
Console.WriteLine("✓ EventCallback - Parent can respond to child events");
Console.WriteLine("✓ Component reuse - Same ProductCard, different data");
Console.WriteLine("✓ Conditional rendering - @if for In Stock badge");
Console.WriteLine("\n✓ ONE component definition → MANY instances!");