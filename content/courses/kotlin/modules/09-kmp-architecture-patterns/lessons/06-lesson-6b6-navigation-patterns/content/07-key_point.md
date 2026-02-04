---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Navigation is inherently platform-specific**—Android uses NavController, iOS uses UINavigationController, web uses router libraries. Don't try to share navigation logic; share navigation state instead.

**Emit navigation events from ViewModels as SharedFlow**—one-time events like "navigate to detail screen" or "go back". Platform code consumes these events and triggers platform-specific navigation.

**Pass data via explicit navigation parameters**, not global state. When navigating to a detail screen, pass the item ID so the destination can load its own data—this keeps screens decoupled and deep-linkable.
