---
type: "EXAMPLE"
title: "ProGuard Troubleshooting"
---


Common ProGuard issues and fixes:



```proguard
# Issue: ClassNotFoundException at runtime
# Cause: ProGuard removed or renamed a class used via reflection
# Solution: Add keep rule
-keep class com.example.MyReflectedClass { *; }

# Issue: NoSuchMethodError at runtime
# Cause: Method was removed or renamed
# Solution: Keep the specific class and its methods
-keepclassmembers class com.example.MyClass {
    public void myMethod(...);
}

# Issue: Gson/JSON parsing fails
# Cause: Model class fields were renamed
# Solution: Keep all model classes
-keep class com.example.myapp.models.** { *; }

# Issue: Firebase crashes
# Solution: Add Firebase-specific rules
-keep class com.google.firebase.** { *; }
-keep class com.google.android.gms.** { *; }
-dontwarn com.google.firebase.**

# Issue: Retrofit/OkHttp issues
# Solution: Add networking rules
-keepattributes Signature
-keepattributes Exceptions
-keep class retrofit2.** { *; }
-keep class okhttp3.** { *; }

# Debug ProGuard issues: Disable temporarily
# In build.gradle, set minifyEnabled false
# If app works without ProGuard, add keep rules until it works with ProGuard
```
