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
// Match 'http' or 'HTTPS' case-insensitively, but domain part is case-sensitive
const url = /(?i:https?):\/\/([a-z0-9.]+)/;
url.exec('HTTP://example.com');   // ['HTTP://example.com', 'example.com'] - matches!
url.exec('HTTP://Example.COM');   // null - domain part is case-sensitive (no 'i' flag)
```