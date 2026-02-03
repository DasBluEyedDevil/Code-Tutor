---
type: "KEY_POINT"
title: "Parameters, Events, and Cascading Values"
---

## Key Takeaways

- **`EventCallback<T>` enables child-to-parent communication** -- the child component invokes the callback, the parent handles the event. This is Blazor's equivalent of raising events.

- **`RenderFragment` accepts child content** -- name the parameter `ChildContent` for the default slot. Parents can pass HTML or other components as content: `<Card><p>Content here</p></Card>`.

- **`[CascadingParameter]` avoids prop drilling** -- wrap an ancestor with `<CascadingValue>` and descendants receive the value without explicit parameter passing through every intermediate component.
