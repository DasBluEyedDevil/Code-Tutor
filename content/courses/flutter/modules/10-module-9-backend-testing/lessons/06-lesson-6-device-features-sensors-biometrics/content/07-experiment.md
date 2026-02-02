---
type: "EXAMPLE"
title: "Best Practices"
---


1. **Always Check Availability**
   ```dart
   final hasVibrator = await Vibration.hasVibrator();
   final canAuth = await _localAuth.canCheckBiometrics;
   ```

2. **Provide Fallback Options**
   - If biometrics fail, offer PIN/password
   - If sensors unavailable, provide manual input

3. **Don't Overuse Haptics**
   - Only for important actions (button taps, errors)
   - Too much vibration annoys users

4. **Cancel Streams**
   ```dart
   @override
   void dispose() {
     _sensorSubscription?.cancel();
     super.dispose();
   }
   ```

5. **Handle Permissions Gracefully**
   - Explain why you need sensor access
   - Provide option to skip if not critical

