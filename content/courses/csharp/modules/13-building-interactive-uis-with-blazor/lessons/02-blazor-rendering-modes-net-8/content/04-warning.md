---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Issues

**Static SSR = No @onclick**: If you forget to set a render mode, components use Static SSR by default. Buttons won't respond to clicks! Add @rendermode for interactivity.

**WebAssembly download size**: First load downloads 5-10MB (.NET runtime + app). Users on slow connections may wait 10+ seconds. Use InteractiveAuto to start fast!

**Server connection required**: InteractiveServer needs constant SignalR connection. Lost internet = frozen UI. Consider InteractiveAuto for better offline resilience.

**Prerendering double execution**: By default, components prerender on server, then render again on client. OnInitialized runs TWICE! Use `prerender: false` or persist state.

**Mixing modes complexity**: Different render modes can't share state directly. Server component can't access WebAssembly component's memory. Use services or cascading parameters.

**.NET 9 update**: Use [ExcludeFromInteractiveRouting] attribute for pages that MUST use Static SSR (like those needing HTTP cookies) in globally interactive apps.