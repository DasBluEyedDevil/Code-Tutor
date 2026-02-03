---
type: "KEY_POINT"
title: "Two-Way Data Binding"
---

## Key Takeaways

- **`@bind="variable"` syncs input and state** -- when the user types, the variable updates. When code changes the variable, the input updates. This two-way binding eliminates manual event wiring.

- **`@bind:event="oninput"` controls update timing** -- override the default `onchange` (blur) behavior to update on every keystroke. Useful for real-time filtering and live previews.

- **Component binding requires a matching `EventCallback`** -- `@bind-Value` on a child component requires both a `Value` parameter and a `ValueChanged` EventCallback parameter for the binding to work.
