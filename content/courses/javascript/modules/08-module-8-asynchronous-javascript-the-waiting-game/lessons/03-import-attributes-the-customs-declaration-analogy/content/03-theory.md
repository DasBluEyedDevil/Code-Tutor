---
type: "THEORY"
title: "Why Import Attributes Matter"
---

Import Attributes (ES2025) solve three problems:

**1. Security**
```javascript
// Without attributes - what if evil.json contains executable code?
import data from './evil.json';  // DANGEROUS

// With attributes - JavaScript enforces it's ONLY JSON
import data from './evil.json' with { type: 'json' };  // SAFE
```

**2. Clarity**
Anyone reading your code knows exactly what type of file you're importing.

**3. Portability**
The same syntax works across Bun, Node.js, Deno, and browsers. No more runtime-specific import hacks!