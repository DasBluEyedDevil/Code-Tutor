import { dlopen, FFIType, suffix } from 'bun:ffi';

// Load libc and call getpid()
// Print: 'Process ID: <pid>'