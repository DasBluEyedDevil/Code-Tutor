---
type: "THEORY"
title: "The Five Scope Functions: Overview"
---


| Function | Context | Return | Common Use |
|----------|---------|--------|------------|
| `let` | `it` | Lambda result | Null safety, transformations |
| `run` | `this` | Lambda result | Object configuration & compute result |
| `with` | `this` | Lambda result | Multiple operations on object |
| `apply` | `this` | Object itself | Object configuration |
| `also` | `it` | Object itself | Side effects (logging, validation) |

### Key Differences

**Context**: How you refer to the object
- `this`: Receiver (implicit, can omit)
- `it`: Parameter (explicit, must use `it`)

**Return value**:
- Lambda result: Returns what the block returns
- Object itself: Returns the original object (chainable)

---

