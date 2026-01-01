---
type: "THEORY"
title: "Common Issues & Solutions"
---


**Issue 1: Tasks not running on iOS**
- **Solution**: iOS is very restrictive. Tasks may not run for hours.
- BGTaskScheduler runs at system discretion
- Test with `e -l objc -- (void)[[BGTaskScheduler sharedScheduler] _simulateLaunchForTaskWithIdentifier:@"your.identifier"]` in Xcode debugger

**Issue 2: Tasks running too frequently**
- **Solution**: Set minimum `frequency: Duration(hours: 1)`
- Android minimum is 15 minutes, but OS may run less frequently

**Issue 3: Task crashes**
- **Solution**: Ensure callback is top-level function with `@pragma('vm:entry-point')`
- Don't access app state directly (use SharedPreferences, SQLite)

**Issue 4: Tasks not running after app force-quit (iOS)**
- **Solution**: This is expected iOS behavior
- iOS doesn't guarantee background execution after force-quit

