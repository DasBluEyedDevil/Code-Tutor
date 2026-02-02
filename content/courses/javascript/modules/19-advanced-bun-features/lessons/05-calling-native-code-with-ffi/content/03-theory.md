---
type: "THEORY"
title: "FFI Types"
---

**Available types:**
```javascript
import { FFIType } from 'bun:ffi';

// Integers
FFIType.i8   // int8_t
FFIType.u8   // uint8_t
FFIType.i16  // int16_t
FFIType.u16  // uint16_t
FFIType.i32  // int32_t  (int)
FFIType.u32  // uint32_t
FFIType.i64  // int64_t  (long long)
FFIType.u64  // uint64_t

// Floats
FFIType.f32  // float
FFIType.f64  // double

// Pointers
FFIType.ptr     // void*
FFIType.cstring // char* (null-terminated)

// Special
FFIType.void    // void (for returns only)
```