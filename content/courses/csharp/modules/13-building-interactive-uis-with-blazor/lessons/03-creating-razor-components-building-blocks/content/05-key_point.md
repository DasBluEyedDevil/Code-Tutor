---
type: "KEY_POINT"
title: "Razor Component Structure"
---

## Key Takeaways

- **Component name must match the filename** -- `Alert.razor` defines the `<Alert />` component. Use PascalCase for both. Components are used as HTML-like tags in parent components.

- **`[Parameter]` exposes properties to parent components** -- `[Parameter] public string Message { get; set; }` lets parents pass data: `<Alert Message="Warning!" />`. Parameters must be public.

- **`OnInitialized()` runs once when the component loads** -- override lifecycle methods for setup logic. Use `OnInitializedAsync()` for async operations like loading data from an API.
