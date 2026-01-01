---
type: "THEORY"
title: "Lesson Summary"
---


### Migration Summary

1. **Update Kotlin to 2.0.21+** in your version catalog
2. **Enable K2 progressively** - language version first, then API version
3. **Check dependencies** - ensure all libraries support Kotlin 2.0
4. **Fix stricter null checks** - K2 catches more null issues
5. **Add explicit types** where inference changes
6. **Migrate kapt to KSP** for better performance

### Testing Your Migration

```bash
# Build and run tests
./gradlew build

# Check for warnings
./gradlew compileKotlin --warning-mode all

# Run your test suite
./gradlew test
```

### Next Steps

In the next lesson, you'll learn about KSP and how to migrate from kapt for faster builds.

---

