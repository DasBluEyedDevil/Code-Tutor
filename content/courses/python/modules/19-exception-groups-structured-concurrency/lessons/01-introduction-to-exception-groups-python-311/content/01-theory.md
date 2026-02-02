---
type: "THEORY"
title: "The Problem: Multiple Failures"
---

Traditional `try/except` handles **one error at a time**. But what happens when multiple things fail simultaneously?

**Real-world scenarios:**
- You're validating 5 form fields, and 3 have errors
- You're calling 10 APIs in parallel, and 4 fail
- You're processing a batch of files, and some are corrupt

**The old way:** Only the first error gets caught. Others are lost or require complex workarounds.

**Python 3.11+ solution:** `ExceptionGroup` - a container that holds multiple exceptions at once.

```python
# Python 3.11+ requirement
import sys
assert sys.version_info >= (3, 11), "Requires Python 3.11+"
```