---
type: "WARNING"
title: "Common Performance Pitfalls"
---


**Avoid These Mistakes:**

1. **Loading animations in build():**
```dart
// BAD - Reloads every rebuild
@override
Widget build(BuildContext context) {
  return Lottie.network('https://...'); // Network call on every build!
}

// GOOD - Load once, cache the composition
LottieComposition? _composition;

@override
void initState() {
  super.initState();
  _loadAnimation();
}

Future<void> _loadAnimation() async {
  final composition = await AssetLottie('assets/anim.json').load();
  setState(() => _composition = composition);
}
```

2. **Too many simultaneous animations:**
   - Each animation has render overhead
   - Consider pausing off-screen animations
   - Use simpler alternatives when many are visible

3. **Ignoring device capabilities:**
   - Test on low-end devices
   - Provide fallback for poor performers
   - Reduce complexity based on device

4. **Not disposing controllers:**
```dart
@override
void dispose() {
  _animationController.dispose(); // Always dispose!
  super.dispose();
}
```

