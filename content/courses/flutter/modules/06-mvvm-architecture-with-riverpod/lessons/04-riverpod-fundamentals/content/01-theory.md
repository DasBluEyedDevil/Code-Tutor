---
type: "THEORY"
title: "Why Riverpod?"
---

You may have heard of the Provider package for state management in Flutter. Riverpod is the next evolution, created by the same author (Remi Rousselet). Let us understand why Riverpod exists and what problems it solves.

### Provider Package Limitations

The original Provider package has served Flutter developers well, but it has some frustrating limitations:

**1. No Compile-Time Safety**
With Provider, errors only appear at runtime:
```dart
// Provider - This compiles fine but crashes at runtime!
final user = context.read<UserProvider>();  // Oops, UserProvider not registered!
```

**2. BuildContext Required Everywhere**
You need a BuildContext to access providers, which means:
- Cannot access state in pure Dart classes
- Cannot easily access state outside the widget tree
- Testing requires building widget trees

**3. Cannot Have Multiple Providers of Same Type**
Trying to provide two `String` values? You need workarounds.

### Riverpod Benefits

Riverpod solves all of these problems:

**1. Compile-Time Safe**
Errors are caught before you run the app:
```dart
// Riverpod - Compiler catches typos and missing providers!
final user = ref.watch(userProvider);  // If userProvider doesn't exist, code won't compile
```

**2. No BuildContext Needed**
Access state from anywhere. Testing is simple. No widget tree required.

**3. Multiple Providers of Same Type**
Create as many String, int, or custom providers as you need. Each has a unique identity.

**4. Better Testing**
Providers can be overridden for testing. No widget setup required.

**5. Automatic Disposal**
Providers can automatically clean up when no longer needed, preventing memory leaks.