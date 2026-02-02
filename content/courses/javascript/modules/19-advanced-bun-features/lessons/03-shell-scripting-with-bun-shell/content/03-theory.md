---
type: "THEORY"
title: "Output Handling"
---

**Get output in different formats:**
```javascript
const cmd = $`echo 'hello'`;

await cmd.text();    // 'hello\n'
await cmd.json();    // Parse as JSON
await cmd.blob();    // As Blob
await cmd.lines();   // ['hello']
await cmd.bytes();   // Uint8Array
```

**Check exit code:**
```javascript
const result = await $`exit 1`.nothrow();
console.log(result.exitCode);  // 1
```

**Quiet mode (suppress output):**
```javascript
await $`npm install`.quiet();
```