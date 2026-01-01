---
type: "THEORY"
title: "Lesson Summary"
---


### KSP Migration Summary

1. **KSP is 2x faster than kapt** - worth the migration effort
2. **Most major libraries support KSP** - Room, Moshi, Dagger, Koin
3. **Migration is usually simple** - change `kapt` to `ksp` in dependencies
4. **Configure KSP arguments** using the `ksp { }` block
5. **You can mix kapt and KSP** during gradual migration
6. **Remove kapt plugin** when fully migrated for best performance

### Migration Command

```bash
# Quick find-and-replace in build files:
# kapt(...) -> ksp(...)
# id("org.jetbrains.kotlin.kapt") -> remove if not needed
```

### Next Steps

In the next lesson, you'll learn how to write your own KSP processor to generate code.

---

