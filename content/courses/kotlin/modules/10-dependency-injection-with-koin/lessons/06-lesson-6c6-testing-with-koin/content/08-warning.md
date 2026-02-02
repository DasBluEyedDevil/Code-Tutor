---
type: "WARNING"
title: "Testing Anti-Patterns"
---

### ❌ Not stopping Koin between tests

```kotlin
@Test
fun test1() {
    startKoin { ... }
    // Forgot stopKoin()
}

@Test
fun test2() {
    startKoin { ... }  // Error: Koin already started!
}
```

**Fix**: Always call `stopKoin()` in `@AfterTest`.

### ❌ Testing with production dependencies

```kotlin
// BAD: Tests hit real database/network
startKoin {
    modules(productionModule)
}
```

**Fix**: Create dedicated test modules with fakes.

### ❌ Not verifying modules

```kotlin
// Module has missing dependency, but no test catches it
val module = module {
    single { ServiceA(get()) }  // Needs ServiceB, not defined
}
```

**Fix**: Add module verification test:
```kotlin
@Test
fun `verify modules`() {
    module.verify()
}
```