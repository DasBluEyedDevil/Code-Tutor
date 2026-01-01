---
type: "KEY_POINT"
title: "ProGuard/R8 Benefits"
---


**What R8 Does (enabled with minifyEnabled true):**

1. **Code Shrinking** - Removes unused classes, methods, and fields
2. **Optimization** - Inlines methods, removes dead code branches
3. **Obfuscation** - Renames classes/methods to short names (a, b, c)
4. **Resource Shrinking** - Removes unused resources (with shrinkResources true)

**Typical Size Reduction:**
- 20-50% smaller APK size
- Faster app startup
- Harder to reverse-engineer

**Potential Issues:**
- Reflection-based code may break (add -keep rules)
- Stack traces use obfuscated names (upload mapping.txt to Play Console)
- Some libraries need specific ProGuard rules

**The mapping.txt file:**
Found in `build/app/outputs/mapping/release/mapping.txt` after building. Upload to Play Console to see readable crash reports.

