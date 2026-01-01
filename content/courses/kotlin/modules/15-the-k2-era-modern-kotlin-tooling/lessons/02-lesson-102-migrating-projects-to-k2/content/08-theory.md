---
type: "THEORY"
title: "Common Migration Issues"
---


### Known Breaking Changes

**1. Overload Resolution**
K2 may resolve overloads differently in rare cases. If you see unexpected method calls, add explicit types.

**2. Visibility Checks**
K2 is stricter about visibility. Internal members from dependencies may become inaccessible.

**3. Type Approximation**
Anonymous types are approximated differently. If types change unexpectedly, add explicit type annotations.

**4. SAM Conversions**
Some edge cases in SAM conversion work differently. Use explicit lambda types if issues occur.

**5. Annotation Processing**
kapt works but is slower. Migrate to KSP for better performance.

---

