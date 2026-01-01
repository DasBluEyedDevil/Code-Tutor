---
type: "THEORY"
title: "🔌 Platform Channels: Talking to Native Code"
---


### What Are Platform Channels?

Sometimes you need features that don't have a Flutter plugin. **Platform Channels** let your Dart code communicate directly with native Android (Kotlin/Java) and iOS (Swift/Objective-C) code.

**Analogy:** Think of Platform Channels like a **translator at a UN meeting**. Dart speaks one language, Android/iOS speak another. The channel translates messages back and forth!

```
┌─────────────────┐                    ┌─────────────────┐
│     FLUTTER     │                    │     NATIVE      │
│    (Dart)       │◄═══════════════════►│  (Kotlin/Swift) │
│                 │   MethodChannel    │                 │
└─────────────────┘                    └─────────────────┘
         │                                      │
    invokeMethod()  ─────────────────►   handle method
         │          ◄─────────────────   return result
    receive result                              │
```

### When Do You Need Platform Channels?

1. **No plugin exists** for the feature you need
2. **Proprietary SDKs** that only have native libraries
3. **Hardware features** not exposed by Flutter
4. **Performance-critical** code that must run natively
5. **Existing native code** you want to reuse

