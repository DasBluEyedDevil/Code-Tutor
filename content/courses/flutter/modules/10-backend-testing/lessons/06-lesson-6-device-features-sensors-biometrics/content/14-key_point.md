---
type: "KEY_POINT"
title: "Answer Key"
---


**Answer 1:** B) User accelerometer filters out gravity

`accelerometerEvents` includes gravity (so when device is still, z-axis shows ~9.8 m/s²). `userAccelerometerEvents` filters out gravity, showing only user-caused motion. Use user accelerometer for gesture detection, regular accelerometer for orientation.

**Answer 2:** B) For important actions like errors or deletions

Heavy impact should be reserved for significant moments like errors, destructive actions (delete), or important confirmations. Overusing strong haptics reduces their effectiveness and annoys users. Light impact is for normal taps.

**Answer 3:** B) Keeps showing the dialog until user interacts

`stickyAuth: true` prevents the authentication dialog from dismissing automatically. It stays visible until the user successfully authenticates or explicitly cancels. This prevents accidental dismissals on Android.

