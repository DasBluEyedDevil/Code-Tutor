---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// SIMPLE PARAMETERS
@code {
    [Parameter]
    public string Title { get; set; } = "Default Title";
    
    [Parameter]
    public int Count { get; set; } = 0;
    
    [Parameter]
    public bool IsVisible { get; set; } = true;
}

// COMPLEX OBJECT PARAMETER
public class Product {
    public string Name { get; set; }
    public decimal Price { get; set; }
}

@code {
    [Parameter]
    public Product ProductData { get; set; }
}
<p>@ProductData.Name: $@ProductData.Price</p>

// COLLECTION PARAMETER
@code {
    [Parameter]
    public List<string> Items { get; set; } = new();
}
@foreach (var item in Items) {
    <li>@item</li>
}

// EVENT CALLBACK PARAMETER
@code {
    [Parameter]
    public EventCallback<int> OnValueChanged { get; set; }
    
    private async Task NotifyParent(int value) {
        await OnValueChanged.InvokeAsync(value);
    }
}

// CHILD CONTENT (RENDERFRAGMENT)
@code {
    [Parameter]
    public RenderFragment ChildContent { get; set; }
}
<div class="card">
    @ChildContent
</div>
// Usage: <Card><p>This goes inside!</p></Card>

// CASCADING PARAMETERS
<CascadingValue Value="@currentUser">
    <ChildComponent />  // Receives currentUser automatically
</CascadingValue>

@code {
    [CascadingParameter]
    public User CurrentUser { get; set; }
}
```
