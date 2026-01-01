---
type: "WARNING"
title: "Compatibility Note"
---

RegExp modifiers are ES2025 but may not be available in all environments yet:

| Runtime | Support |
|---------|--------|
| Bun 1.1+ | Yes |
| Node 23+ | Yes |
| Chrome 125+ | Yes |
| Firefox | Not yet |

For maximum compatibility, consider using separate regex patterns or libraries like `re2` for complex cases.