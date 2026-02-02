---
type: "THEORY"
title: "Module Verification"
---

Koin's `checkModules()` catches configuration errors at test time instead of runtime:

```kotlin
import org.koin.test.verify.verify

class ModuleVerificationTest {
    @Test
    fun `verify all modules`() {
        // This catches:
        // - Missing dependencies
        // - Circular dependencies
        // - Type mismatches
        
        appModule.verify(
            extraTypes = listOf(
                Context::class,        // Platform types provided externally
                SqlDriver::class
            )
        )
    }
}
```

### What It Catches

```kotlin
val badModule = module {
    single { NotesRepository(get()) }  // Needs Database
    // Oops, forgot to define Database!
}

// Test:
badModule.verify()
// Error: Missing definition for type 'Database'
```