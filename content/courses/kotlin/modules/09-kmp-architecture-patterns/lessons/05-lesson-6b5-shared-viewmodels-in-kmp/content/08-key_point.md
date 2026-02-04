---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Shared ViewModels in commonMain work across all platforms** by exposing StateFlow and accepting plain functions for user actions. Platform code observes the state and calls ViewModel functions.

**Lifecycle integration differs per platform**—Compose has lifecycle-aware collection, iOS needs manual observation with Combine/async-await bridges. Abstract these differences in platform-specific presentation layers.

**ViewModels shouldn't reference platform UI types**—no Compose State, no SwiftUI Binding. Keep ViewModels platform-agnostic by using StateFlow and avoiding framework-specific types.
