---
type: "WARNING"
title: "FFI Risks"
---

**FFI is powerful but dangerous:**

1. **Memory safety is your responsibility**
   - Native code can crash your entire process
   - No garbage collection for native memory

2. **Type mismatches cause undefined behavior**
```javascript
// WRONG - passing string where int expected
lib.symbols.add('5', 3);  // Crash or garbage!
```

3. **Always close libraries**
```javascript
lib.close();  // Free resources
```

**When to use FFI:**
- Performance-critical code (image processing, crypto)
- Interfacing with system libraries
- Reusing existing native code

**When NOT to use FFI:**
- Simple operations (JavaScript is fast enough)
- When a npm package already wraps the native code