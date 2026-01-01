---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Issues

**Foreach variable capture**: In foreach, use lambda carefully! `@onclick="() => Delete(item)"` captures reference. If collection changes, wrong item deleted! Copy to local: `var localItem = item;`

**Event bubbling**: Click on child AND parent fires both handlers! Use `@onclick:stopPropagation` to prevent bubbling. Or handle in only one place.

**Async void danger**: Don't use `async void` for event handlers! Exceptions are lost. Use `async Task` and Blazor handles it: `private async Task HandleClick() { ... }`

**@oninput performance**: Fires on EVERY keystroke! In large apps, debounce or use @onchange. Every keystroke = re-render.

**Missing await in async handler**: `await OnClick.InvokeAsync()` - forgetting await means parent handler might not complete before continuing.

**Wrong event args type**: MouseEventArgs for mouse, KeyboardEventArgs for keyboard, ChangeEventArgs for input. Wrong type = runtime error. Check docs for correct type.