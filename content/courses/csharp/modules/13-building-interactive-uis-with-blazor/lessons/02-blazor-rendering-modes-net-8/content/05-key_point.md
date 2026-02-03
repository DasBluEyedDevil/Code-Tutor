---
type: "KEY_POINT"
title: "Rendering Mode Selection"
---

## Key Takeaways

- **Static SSR is the default** -- when no `@rendermode` is specified, components render as plain HTML with no interactivity. Use this for content pages where SEO and fast loading matter most.

- **`InteractiveAuto` gives the best user experience** -- it starts with server-side rendering for fast initial load, then transparently switches to WebAssembly once downloaded. One directive, two benefits.

- **Each mode has tradeoffs** -- Server requires a persistent SignalR connection. WebAssembly has a large initial download. Static has no interactivity. Choose based on your specific page requirements.
