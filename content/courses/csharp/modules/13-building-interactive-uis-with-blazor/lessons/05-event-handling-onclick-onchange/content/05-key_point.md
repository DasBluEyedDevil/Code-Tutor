---
type: "KEY_POINT"
title: "Blazor Event Handling"
---

## Key Takeaways

- **Use lambdas to pass parameters** -- `@onclick="() => Delete(item.Id)"` wraps the call in a lambda so the parameter is captured. Without the lambda, the method would execute immediately during rendering.

- **`@oninput` fires on every keystroke, `@onchange` on blur** -- use `@oninput` for live search/filtering. Use `@onchange` when you only need the final value after the user leaves the field.

- **Blazor re-renders automatically after events** -- when your event handler modifies state, the component re-renders to reflect changes. No manual DOM manipulation needed.
