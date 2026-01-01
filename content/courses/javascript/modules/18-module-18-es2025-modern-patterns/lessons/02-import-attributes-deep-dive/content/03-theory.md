---
type: "THEORY"
title: "Why Not Just Parse JSON Manually?"
---

You might wonder: why not just use `JSON.parse(Bun.file('./config.json').text())`?

**Import attributes offer:**

1. **Static analysis**: Bundlers know what you're importing at build time
2. **Caching**: Imported JSON is cached like any other module
3. **Security**: Prevents code injection through JSON files
4. **Portability**: Same syntax works in Bun, Node, Deno, browsers

**Without import attributes:**
```javascript
// Node 18: This could execute code if evil.json contains a ".default" export!
import data from './evil.json';  // DANGEROUS in some runtimes
```

**With import attributes:**
```javascript
// All runtimes: Guaranteed to be pure JSON, no code execution
import data from './evil.json' with { type: 'json' };
```