---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`.razor file`**: Blazor components use .razor extension. Mix HTML markup with C# code. Top part = markup, @code block = C# logic.

**`@variable`**: @ symbol in markup accesses C# variables/expressions. @currentCount displays the value. @DateTime.Now.Year shows current year.

**`@onclick="MethodName"`**: Event binding with @. @onclick for clicks, @onchange for input changes. Binds to C# methods, not JavaScript!

**`@code { }`**: Contains C# logic for component. Define fields, properties, methods here. Private by default. This is your component's brain!