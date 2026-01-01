---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// BLAZOR COMPONENT (.razor file)
// Counter.razor

<h3>Counter Example</h3>

<p>Current count: @currentCount</p>

<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;  // C# code!
    }
}

// WHAT MAKES THIS SPECIAL?
// 1. HTML markup at top
// 2. @code block with C# logic
// 3. @onclick binds to C# method (not JavaScript!)
// 4. @currentCount displays C# variable

// COMPARISON TO JAVASCRIPT FRAMEWORKS

// React (JavaScript):
/*
import { useState } from 'react';

function Counter() {
  const [count, setCount] = useState(0);
  return (
    <div>
      <p>Count: {count}</p>
      <button onClick={() => setCount(count + 1)}>Click me</button>
    </div>
  );
}
*/

// Blazor (C#):
/*
<p>Count: @count</p>
<button @onclick="() => count++">Click me</button>

@code {
    private int count = 0;
}
*/

// BENEFITS OF BLAZOR
// ✅ One language (C#) for everything
// ✅ Full .NET ecosystem (NuGet, LINQ, async/await)
// ✅ Type safety (compile-time errors!)
// ✅ Great tooling (Visual Studio, Rider)
// ✅ Code sharing (models, logic, validation)
// ✅ No JavaScript transpiling/bundling complexity
```
