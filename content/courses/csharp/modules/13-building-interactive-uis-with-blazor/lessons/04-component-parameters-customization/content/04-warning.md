---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Issues

**No [Required] attribute**: Blazor has no built-in required parameter validation! Check manually in OnParametersSet: `if (string.IsNullOrEmpty(Name)) throw new InvalidOperationException("Name required");`

**Two-way binding naming**: For @bind-Value, you need BOTH `[Parameter] public T Value { get; set; }` AND `[Parameter] public EventCallback<T> ValueChanged { get; set; }`. Exact naming required!

**EventCallback vs Action**: Use EventCallback, not Action! EventCallback triggers StateHasChanged automatically. Action won't re-render the component.

**RenderFragment naming**: Name it exactly 'ChildContent' for implicit slot: `<MyComponent><p>This works!</p></MyComponent>`. Other names need explicit: `<MyComponent><Header>...</Header></MyComponent>`.

**CascadingParameter performance**: Don't cascade frequently-changing values! Every change re-renders all consumers. Use for stable data like themes, auth state.

**Reference type parameters**: Object parameters share reference! Modifying child affects parent. Use immutable objects or clone if needed.