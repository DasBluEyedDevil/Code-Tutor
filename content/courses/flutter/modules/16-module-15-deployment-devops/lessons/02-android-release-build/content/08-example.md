---
type: "EXAMPLE"
title: "ProGuard Configuration"
---


ProGuard (now R8) shrinks, optimizes, and obfuscates your code. Flutter requires specific rules to work correctly:

**android/app/proguard-rules.pro:**



```proguard
# Flutter-specific rules
-keep class io.flutter.app.** { *; }
-keep class io.flutter.plugin.** { *; }
-keep class io.flutter.util.** { *; }
-keep class io.flutter.view.** { *; }
-keep class io.flutter.** { *; }
-keep class io.flutter.plugins.** { *; }

# Keep Dart entry points
-keep class com.example.myapp.MainActivity { *; }

# Firebase (if using)
-keep class com.google.firebase.** { *; }
-keep class com.google.android.gms.** { *; }

# Gson (if using for JSON)
-keepattributes Signature
-keepattributes *Annotation*
-keep class com.google.gson.** { *; }

# Prevent obfuscation of classes used via reflection
-keepattributes InnerClasses
-keepattributes EnclosingMethod

# Common third-party libraries
# Add rules for any native Android libraries you use
```
