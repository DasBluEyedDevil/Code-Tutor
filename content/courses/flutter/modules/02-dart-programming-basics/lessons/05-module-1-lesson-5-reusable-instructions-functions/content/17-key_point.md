---
type: KEY_POINT
---

- Define functions with a return type, name, and parameter list: `String greet(String name)` makes the contract explicit
- Use `void` as the return type when a function performs an action but does not produce a value
- Named parameters (`{required String name}`) improve call-site readability and let the compiler enforce required arguments
- Keep functions short and single-purpose -- if a function does two things, split it into two functions
- Arrow syntax (`=> expression`) is ideal for one-liner functions that return a single value
