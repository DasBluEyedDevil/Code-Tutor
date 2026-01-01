---
type: "THEORY"
title: "Lazy Injection"
---

Defer dependency resolution for performance or circular dependency handling:

```kotlin
class NotesViewModel(
    // Immediate injection
    private val repository: NotesRepository,
    
    // Lazy injection - only resolved when first accessed
    private val analyticsLazy: Lazy<AnalyticsProvider>
) {
    // Access lazy value
    private val analytics by analyticsLazy
    
    fun onNoteCreated() {
        // Analytics resolved here, not at construction
        analytics.logEvent("note_created")
    }
}

// In module
val module = module {
    viewModel {
        NotesViewModel(
            repository = get(),
            analyticsLazy = inject()  // Returns Lazy<AnalyticsProvider>
        )
    }
}
```

### Breaking Circular Dependencies

```kotlin
// Problem: A needs B, B needs A
class ServiceA(private val b: ServiceB)  // Needs B first
class ServiceB(private val a: ServiceA)  // Needs A first

// Solution: Lazy injection
class ServiceA(private val bLazy: Lazy<ServiceB>) {
    private val b by bLazy
}

class ServiceB(private val a: ServiceA)

val module = module {
    single { ServiceA(inject()) }  // Lazy<ServiceB>
    single { ServiceB(get()) }      // ServiceA (already created)
}
```