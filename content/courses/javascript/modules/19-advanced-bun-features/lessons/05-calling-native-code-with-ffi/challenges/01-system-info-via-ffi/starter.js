// NOTE: Requires Bun runtime AND a native C library (libc).
// bun:ffi has no Node.js equivalent -- Node uses node-ffi-napi (npm package) instead.
// This challenge is conceptual in non-Bun environments.
// Run with: bun run starter.js

import { dlopen, FFIType, suffix } from 'bun:ffi';

// Load libc and call getpid()
// Print: 'Process ID: <pid>'