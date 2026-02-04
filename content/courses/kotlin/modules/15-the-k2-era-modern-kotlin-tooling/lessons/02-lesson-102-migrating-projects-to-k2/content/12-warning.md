---
type: "WARNING"
title: "K2 Migration Compatibility Issues"
---

**K2's stricter type inference can break code** that relied on the old compiler's leniency. Most breaks are actually bug fixes—K2 catches errors the old compiler missed.

**Common migration issues:**

**1. Smart cast changes**—K2 is smarter about smart casts but more conservative in some cases:
```kotlin
var prop: String? = "test"
if (prop != null) {
    // Old compiler: smart cast works
    // K2: can't smart cast var properties (they might change)
    println(prop.length)  // Error in K2
}
```

Fix: use `val` instead of `var`, or assign to local `val`.

**2. Type inference with SAM constructors**:
```kotlin
// May need explicit types with K2
val comparator = Comparator { a, b -> a - b }  // Might fail
val comparator = Comparator<Int> { a, b -> a - b }  // Explicit type
```

**3. Overload resolution changes**—K2 may pick different overloads than the old compiler in ambiguous cases. Add explicit types to disambiguate.

**4. Extension function conflicts** with member functions resolved differently.

**Test extensively after K2 migration**—even if compilation succeeds, behavioral changes are possible (though rare). Run full test suite and monitor production.

Most projects migrate smoothly, but large codebases with complex generics may encounter issues.
