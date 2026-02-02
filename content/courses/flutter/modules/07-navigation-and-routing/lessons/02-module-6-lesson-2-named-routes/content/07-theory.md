---
type: "THEORY"
title: "Named Routes vs Basic Navigation"
---


| Feature | Basic Navigation | Named Routes |
|---------|------------------|--------------|
| **Setup** | None | Define routes upfront |
| **Navigate** | `Navigator.push(MaterialPageRoute(...))` | `Navigator.pushNamed('/route')` |
| **Arguments** | Constructor params | `arguments` parameter |
| **Type Safety** | ✓ Compile-time | Runtime (unless using constants) |
| **Centralized** | ✗ No | ✓ Yes |
| **Best For** | Small apps | Medium-large apps |

