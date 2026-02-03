---
type: "KEY_POINT"
title: "Blazor Component Model"
---

## Key Takeaways

- **Blazor components use `.razor` files** -- mix HTML markup with C# code. The `@code { }` block contains your component logic. The `@` symbol accesses C# expressions in markup.

- **Event handling is pure C#** -- `@onclick="HandleClick"` calls a C# method, not JavaScript. Blazor handles the DOM updates automatically after your method runs.

- **Blazor supports Server, WebAssembly, and Auto modes** -- choose based on your needs: Server for fast initial load with persistent connection, WebAssembly for fully client-side offline capability, Auto for the best of both.
