---
type: "WARNING"
title: "Common Mistakes"
---

❌ **Mistake 1**: Using `MaterialApp` instead of `MaterialApp.router`

✅ **Fix**: Use `MaterialApp.router(routerConfig: goRouter)` instead of regular `MaterialApp()`. GoRouter requires the router-aware constructor to manage navigation state properly.

```dart
// Wrong
MaterialApp(home: HomeScreen());

// Correct
MaterialApp.router(routerConfig: goRouter);
```

❌ **Mistake 2**: Forgetting slashes in paths

✅ **Fix**: Always start paths with `/`, e.g., `'/settings'` not `'settings'`. GoRouter treats paths as URL segments, and missing leading slashes cause route matching failures.

```dart
// Wrong
GoRoute(path: 'settings', ...)

// Correct
GoRoute(path: '/settings', ...)
```

❌ **Mistake 3**: Using `Navigator.push` instead of `context.go`

✅ **Fix**: Use `context.go('/route')` or `context.push('/route')` with GoRouter methods. Mixing Navigator with GoRouter bypasses GoRouter's routing logic and breaks URL synchronization.

```dart
// Wrong
Navigator.push(context, MaterialPageRoute(builder: (_) => SettingsScreen()));

// Correct
context.go('/settings');    // Replaces current route
context.push('/settings');  // Pushes onto stack
```
