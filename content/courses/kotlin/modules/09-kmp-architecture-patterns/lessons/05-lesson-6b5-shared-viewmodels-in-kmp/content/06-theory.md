---
type: "THEORY"
title: "iOS Integration with SKIE"
---

### Modern Approach: SKIE Library

[SKIE](https://skie.touchlab.co/) by Touchlab makes Kotlin Flows work natively in Swift:

```swift
// Without SKIE - complex callback handling
viewModel.state.collect { state in
    // Update UI
}

// With SKIE - native Swift async/await
for await state in viewModel.state {
    // Update UI
}
```

### Setup SKIE

```kotlin
// build.gradle.kts
plugins {
    id("co.touchlab.skie") version "0.9.0"
}

skie {
    features {
        enableSwiftUIObservingPreview = true
    }
}
```

### Benefits

1. **Native Swift experience** - Flows become AsyncSequences
2. **SwiftUI integration** - Automatic @Published support
3. **Type-safe** - Better Swift interop
4. **Cancellation** - Proper Swift Task cancellation