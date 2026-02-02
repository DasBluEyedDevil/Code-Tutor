---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Wrap individual operations** to catch failures without stopping the group
- Return `(status, result)` tuples to distinguish success/failure
- **Collect both successes and failures** separately
- Let the **caller decide** how to handle partial success
- Use dataclasses for structured error information