---
type: "EXAMPLE"
title: "Calling C Functions"
---

Load and call functions from shared libraries:

```javascript
import { dlopen, FFIType, suffix } from 'bun:ffi';

// Load a system library (libc)
const lib = dlopen(`libc.${suffix}`, {
  // Define the function signature
  strlen: {
    args: [FFIType.cstring],  // Takes a C string
    returns: FFIType.i32,      // Returns int32
  },
  getpid: {
    args: [],
    returns: FFIType.i32,
  },
});

// Call the functions
const length = lib.symbols.strlen('Hello, Bun!');
console.log('String length:', length);  // 11

const pid = lib.symbols.getpid();
console.log('Process ID:', pid);

// Don't forget to close when done
lib.close();
```
