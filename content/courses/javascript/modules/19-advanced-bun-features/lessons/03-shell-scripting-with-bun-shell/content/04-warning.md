---
type: "WARNING"
title: "Security & Escaping"
---

Bun Shell automatically escapes interpolated values:

```javascript
const userInput = 'file; rm -rf /';
await $`cat ${userInput}`;  // Safe! Escapes the semicolon
```

**But be careful with:**
```javascript
// DANGEROUS - raw() bypasses escaping
await $`${$.raw(untrustedInput)}`;  // Never do this!

// SAFE - let Bun escape it
await $`${untrustedInput}`;  // Bun escapes automatically
```

**Additional security notes:**
- Strings starting with `-` may be interpreted as flags - validate input
- Keep Bun updated to get security patches
- Never pass untrusted input directly to shell commands without validation