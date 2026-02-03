// NOTE: Requires Bun runtime AND a native C library (libc).
// bun:ffi has no Node.js equivalent -- Node uses node-ffi-napi (npm package) instead.
// This challenge is conceptual in non-Bun environments.
// Run with: bun run solution.js

import { dlopen, FFIType, suffix } from 'bun:ffi';

const libc = dlopen(`libc.${suffix}`, {
  getpid: {
    args: [],
    returns: FFIType.i32,
  },
});

const pid = libc.symbols.getpid();
console.log(`Process ID: ${pid}`);

libc.close();