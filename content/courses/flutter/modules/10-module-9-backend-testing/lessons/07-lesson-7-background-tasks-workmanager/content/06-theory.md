---
type: "THEORY"
title: "Advanced Features"
---


### 1. Task Constraints


### 2. Initial Delay


### 3. Backoff Policy


**Backoff Example:**
- First retry: after 30 seconds
- Second retry: after 60 seconds (exponential)
- Third retry: after 120 seconds

### 4. Replacing vs Keeping Existing Tasks


- **replace**: Cancel old task, register new one
- **keep**: Keep old task, ignore new registration
- **append**: Run both (rarely used)



```dart
await Workmanager().registerPeriodicTask(
  'my-task',
  'syncData',
  existingWorkPolicy: ExistingWorkPolicy.replace,  // or .keep, .append
);
```
