---
type: "THEORY"
title: "Collecting on Android"
---

### In ViewModel with viewModelScope
```kotlin
// androidMain or Android app module
class AndroidProfileViewModel(
    private val sharedViewModel: ProfileViewModel
) : ViewModel() {
    
    val uiState = sharedViewModel.uiState
        .stateIn(
            scope = viewModelScope,
            started = SharingStarted.WhileSubscribed(5000),
            initialValue = ProfileUiState.Loading
        )
}
```

### In Compose with collectAsStateWithLifecycle
```kotlin
import androidx.lifecycle.compose.collectAsStateWithLifecycle

@Composable
fun ProfileScreen(viewModel: ProfileViewModel) {
    val uiState by viewModel.uiState.collectAsStateWithLifecycle()
    
    when (val state = uiState) {
        is ProfileUiState.Loading -> LoadingIndicator()
        is ProfileUiState.Success -> ProfileContent(state.user)
        is ProfileUiState.Error -> ErrorMessage(state.message)
    }
}
```

### Lifecycle-Aware Collection
```kotlin
// In Fragment or Activity
lifecycleScope.launch {
    repeatOnLifecycle(Lifecycle.State.STARTED) {
        viewModel.uiState.collect { state ->
            updateUI(state)
        }
    }
}
```