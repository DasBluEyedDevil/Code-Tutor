---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// SIMPLE COMPONENT
// Alert.razor
<div class="alert alert-info">
    <p>@Message</p>
</div>

@code {
    [Parameter]
    public string Message { get; set; } = "";
}

// USING THE COMPONENT
// Home.razor
<Alert Message="Welcome to Blazor!" />
<Alert Message="This is reusable!" />

// COMPONENT WITH LOGIC
// Counter.razor
<div class="counter-box">
    <h4>@Title</h4>
    <p>Count: @currentCount</p>
    <button @onclick="Increment">+</button>
    <button @onclick="Decrement">-</button>
    <button @onclick="Reset">Reset</button>
</div>

@code {
    [Parameter]
    public string Title { get; set; } = "Counter";
    
    [Parameter]
    public int InitialValue { get; set; } = 0;
    
    private int currentCount;
    
    protected override void OnInitialized()
    {
        currentCount = InitialValue;
    }
    
    private void Increment() => currentCount++;
    private void Decrement() => currentCount--;
    private void Reset() => currentCount = InitialValue;
}

// USING WITH PARAMETERS
<Counter Title="Score" InitialValue="100" />
<Counter Title="Lives" InitialValue="3" />

// COMPONENT LIFECYCLE
/*
1. OnInitialized() - Component created
2. OnParametersSet() - Parameters received
3. Render - UI drawn
4. OnAfterRender() - After render complete
5. Dispose() - Component destroyed
*/
```
