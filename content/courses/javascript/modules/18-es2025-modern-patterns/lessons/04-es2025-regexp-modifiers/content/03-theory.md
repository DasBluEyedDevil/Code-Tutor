---
type: "THEORY"
title: "Available Modifiers"
---

**Modifiable flags:**
- `i` - case-insensitive
- `m` - multiline (^ and $ match line starts/ends)
- `s` - dotAll (. matches newlines)

**Syntax:**
- `(?i:...)` - Enable flag i for this group
- `(?-i:...)` - Disable flag i for this group
- `(?i-s:...)` - Enable i, disable s
- `(?:...)` - Non-capturing group (unchanged)

**Why this matters:**
Previously, flags were all-or-nothing. Now you can have fine-grained control:

```javascript
// Match 'http' or 'HTTPS' but capture exact case of domain
const url = /(?i:https?):\/\/([a-z0-9.]+)/;
url.exec('HTTP://Example.COM');  // ['HTTP://Example.COM', 'example.com'] - domain lowercased
```