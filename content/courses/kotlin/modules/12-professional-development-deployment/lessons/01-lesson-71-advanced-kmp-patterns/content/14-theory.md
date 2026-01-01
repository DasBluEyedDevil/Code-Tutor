---
type: "THEORY"
title: "Exercise 2: Create a Platform Logger"
---


Build a logging utility that uses platform-specific logging mechanisms.

### Requirements

1. Create `Logger` expect class with methods:
   - `debug(tag: String, message: String)`
   - `info(tag: String, message: String)`
   - `warning(tag: String, message: String)`
   - `error(tag: String, message: String, throwable: Throwable?)`

2. Android: Use `android.util.Log`
3. iOS: Use `NSLog`
4. JVM: Use `println` with timestamps

---

