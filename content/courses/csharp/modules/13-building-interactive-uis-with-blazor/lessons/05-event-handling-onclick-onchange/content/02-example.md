---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// CLICK EVENTS
<button @onclick="HandleClick">Click Me</button>
<button @onclick="() => count++">Increment</button>
<button @onclick="@(() => DoWork(5))">Pass Parameter</button>

@code {
    private void HandleClick() {
        Console.WriteLine("Button clicked!");
    }
    
    private void DoWork(int value) {
        Console.WriteLine($"Working with {value}");
    }
}

// CHANGE EVENTS
<input @onchange="HandleNameChange" />
<select @onchange="HandleCategoryChange">
    <option>Category 1</option>
</select>

@code {
    private void HandleNameChange(ChangeEventArgs e) {
        string newValue = e.Value.ToString();
        Console.WriteLine($"Name changed to: {newValue}");
    }
}

// INPUT EVENTS (every keystroke)
<input @oninput="HandleInput" />
<p>You typed: @currentInput</p>

@code {
    private string currentInput = "";
    
    private void HandleInput(ChangeEventArgs e) {
        currentInput = e.Value.ToString();
    }
}

// KEYBOARD EVENTS
<input @onkeydown="HandleKeyDown" @onkeyup="HandleKeyUp" />

@code {
    private void HandleKeyDown(KeyboardEventArgs e) {
        if (e.Key == "Enter") {
            Console.WriteLine("Enter pressed!");
        }
    }
}

// MOUSE EVENTS
<div @onmouseover="() => isHovered = true" 
     @onmouseout="() => isHovered = false"
     class="@(isHovered ? \"highlight\" : \"\")">
    Hover over me!
</div>

// FORM SUBMIT
<EditForm Model="@person" OnValidSubmit="HandleSubmit">
    <button type="submit">Submit</button>
</EditForm>

@code {
    private Person person = new();
    
    private void HandleSubmit() {
        Console.WriteLine("Form submitted!");
    }
}
```
