---
type: "THEORY"
title: "Railway Operations"
---


### Common Track Operations

| Operation | Purpose | Track Behavior |
|-----------|---------|----------------|
| `map` | Transform success value | Stays on success track |
| `flatMap` | Chain with another Either | Can switch to failure |
| `mapLeft` | Transform error value | Stays on failure track |
| `fold` | Handle both tracks | Exits the railway |
| `getOrElse` | Exit with default | Exits the railway |
| `recover` | Switch from failure to success | Leaves failure track |

---

