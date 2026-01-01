---
type: "THEORY"
title: "Basic Workmanager Usage"
---


### Step 1: Initialize Workmanager


### Step 2: Register One-Time Tasks


### Step 3: Register Periodic Tasks


**Important:** Android minimum periodic interval is **15 minutes**. iOS is even less predictable!



```dart
Future<void> _registerPeriodicTask() async {
  await Workmanager().registerPeriodicTask(
    'periodic-sync',  // Unique ID
    'syncData',       // Task name
    frequency: Duration(hours: 1),  // Run every hour (minimum 15 minutes)
    constraints: Constraints(
      networkType: NetworkType.connected,     // Require internet
      requiresBatteryNotLow: true,            // Don't run if battery low
      requiresCharging: false,                // Run even when not charging
      requiresDeviceIdle: false,              // Run even when device in use
      requiresStorageNotLow: true,            // Don't run if storage low
    ),
    inputData: {
      'periodic': true,
    },
    existingWorkPolicy: ExistingWorkPolicy.replace,  // Replace existing task
  );

  print('Periodic task registered!');
}
```
