---
type: "EXAMPLE"
title: "Custom Native Libraries"
---

Call your own Rust/C code:

```javascript
// Assume you compiled a Rust library: libmath.dylib
// #[no_mangle]
// pub extern "C" fn add(a: i32, b: i32) -> i32 { a + b }

import { dlopen, FFIType, suffix } from 'bun:ffi';

const mathLib = dlopen(`./libmath.${suffix}`, {
  add: {
    args: [FFIType.i32, FFIType.i32],
    returns: FFIType.i32,
  },
  multiply: {
    args: [FFIType.f64, FFIType.f64],
    returns: FFIType.f64,
  },
});

console.log(mathLib.symbols.add(5, 3));       // 8
console.log(mathLib.symbols.multiply(2.5, 4)); // 10.0
```
