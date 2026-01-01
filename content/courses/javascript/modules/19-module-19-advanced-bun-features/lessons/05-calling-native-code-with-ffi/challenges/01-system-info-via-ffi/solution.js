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