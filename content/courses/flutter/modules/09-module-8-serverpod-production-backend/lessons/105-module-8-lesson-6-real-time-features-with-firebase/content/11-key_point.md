---
type: "KEY_POINT"
title: "Answer Key"
---


### Answer 1: B
**Correct**: It automatically rebuilds the UI when data changes

StreamBuilder listens to Firestore snapshots (a Stream) and automatically rebuilds its child widget whenever new data arrives, providing seamless real-time updates without manual setState() calls.

### Answer 2: B
**Correct**: It automatically sets user offline when they lose connection

onDisconnect() is a Firebase Realtime Database feature that executes specified operations when a client disconnects (app closes, network lost, etc.), ensuring accurate presence status even if the app crashes.

### Answer 3: C
**Correct**: Properly dispose streams and controllers

Always cancel stream subscriptions and dispose controllers in dispose() method to prevent memory leaks. Unmanaged streams continue consuming resources even after widgets are destroyed.

