---
type: "WARNING"
title: "Runtime Support"
---

Import attributes are ES2025 but runtime support varies:

| Runtime | JSON | CSS | Other |
|---------|------|-----|-------|
| Bun 1.0+ | Full | Full | - |
| Node 22+ | Full | - | - |
| Deno 1.37+ | Full | - | - |
| Chrome 123+ | Full | Full | - |

For older runtimes, you may need to fall back to:
```javascript
// Fallback for older Node.js
import { readFileSync } from 'fs';
const config = JSON.parse(readFileSync('./config.json', 'utf-8'));
```