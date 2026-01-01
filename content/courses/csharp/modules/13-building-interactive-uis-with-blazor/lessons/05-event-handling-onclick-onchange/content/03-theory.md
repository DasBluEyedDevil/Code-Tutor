---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`@onclick="MethodName"`**: Calls C# method on click. No parentheses if no parameters. For lambda: @onclick="() => code". Event fires, method runs, UI updates.

**`ChangeEventArgs`**: Event argument object. Use e.Value for new value. Different types: KeyboardEventArgs, MouseEventArgs, FocusEventArgs, etc.

**`@oninput vs @onchange`**: @oninput fires on every keystroke. @onchange fires when input loses focus (blur). Use @oninput for live updates, @onchange for final value.

**`Event with parameters`**: Use lambda to pass parameters: @onclick="() => Delete(item.Id)". Or: @onclick="@(() => Process(item))". Lambda wraps method call.