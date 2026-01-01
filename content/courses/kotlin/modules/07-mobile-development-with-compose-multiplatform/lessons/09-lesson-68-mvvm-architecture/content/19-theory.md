---
type: "THEORY"
title: "MVVM on iOS"
---


### Cross-Platform Architecture

MVVM works identically on both Android and iOS with Compose Multiplatform!

| Component | Android | iOS |
|-----------|---------|-----|
| **ViewModel** | Same code | Same code |
| **StateFlow** | Same code | Same code |
| **Repository** | Same code | Same code |
| **DI (Koin)** | Same code | Same code |

### Koin for Cross-Platform DI

Use Koin instead of Hilt for multiplatform apps:

```kotlin
// In commonMain - works on both platforms!
val appModule = module {
    single { UserRepository(get()) }
    viewModel { UsersViewModel(get()) }
}

@Composable
fun UsersScreen(
    viewModel: UsersViewModel = koinViewModel()
) {
    val uiState by viewModel.uiState.collectAsState()
    // Same UI code works on Android and iOS!
}
```

### Running Architecture on iOS

1. Build and run on iOS Simulator
2. Verify ViewModels manage state correctly
3. Test that UI updates reactively
4. Confirm the same architecture works seamlessly!

---

