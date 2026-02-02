---
type: "EXAMPLE"
title: "Best Practices"
---


1. **Keep Background Work Short**
   - ❌ Don't run tasks for > 10 minutes
   - ✅ Break large work into smaller chunks

2. **Handle Task Failures Gracefully**
   ```dart
   try {
     await _performBackgroundWork();
     return Future.value(true);  // Success
   } catch (e) {
     print('Task failed: $e');
     return Future.value(false);  // Will retry with backoff
   }
   ```

3. **Use Constraints to Save Battery**
   ```dart
   // Good: Only sync when on WiFi and charging
   Constraints(
     networkType: NetworkType.unmetered,
     requiresCharging: true,
   )
   ```

4. **Don't Rely on Exact Timing**
   - OS decides when to run tasks
   - iOS is especially unpredictable
   - Use for deferrable work only

5. **Test on Real Devices**
   - Emulators don't accurately simulate background restrictions
   - Test with low battery, airplane mode, etc.

