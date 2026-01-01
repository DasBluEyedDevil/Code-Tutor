---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// SIMPLE BINDING
<input @bind="name" />
<p>Hello, @name!</p>

@code {
    private string name = "";
}

// BINDING WITH DIFFERENT TYPES
<input @bind="age" type="number" />
<input @bind="birthDate" type="date" />
<input type="checkbox" @bind="isActive" />
<select @bind="category">
    <option>Electronics</option>
    <option>Clothing</option>
</select>

@code {
    private int age;
    private DateTime birthDate = DateTime.Now;
    private bool isActive;
    private string category = "Electronics";
}

// BINDING EVENTS
<input @bind="searchTerm" @bind:event="oninput" />
// Updates on every keystroke!

<input @bind="email" @bind:event="onchange" />
// Updates when focus lost (default)

// BINDING WITH FORMATTING
<input @bind="price" @bind:format="C2" />
// Displays as currency: $99.99

<input @bind="date" @bind:format="yyyy-MM-dd" />
// Custom date format

// COMPONENT TWO-WAY BINDING
// Parent:
<Counter @bind-Count="myCount" />
<p>Parent knows: @myCount</p>

// Counter component:
@code {
    [Parameter]
    public int Count { get; set; }
    
    [Parameter]
    public EventCallback<int> CountChanged { get; set; }
    
    private async Task IncrementCount() {
        Count++;
        await CountChanged.InvokeAsync(Count);
    }
}
// Naming: Count + CountChanged
```
