---
type: "THEORY"
title: "Configuration Cache"
---


### What It Caches

The configuration cache stores:
- Resolved plugins
- Applied configurations
- Task graph

### Requirements

Plugins must be compatible:
- No `project` access at execution time
- No shared mutable state
- Proper input/output declarations

### Checking Compatibility

```bash
# Test configuration cache compatibility
./gradlew build --configuration-cache

# See problems report
open build/reports/configuration-cache/
```

---

