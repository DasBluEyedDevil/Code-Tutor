---
type: "THEORY"
title: "Collecting on iOS with SKIE"
---

**SKIE** (Swift Kotlin Interface Enhancer) makes flows native to Swift:

### Setup in build.gradle.kts
```kotlin
plugins {
    id("co.touchlab.skie") version "0.10.9"
}
```

### Swift Usage (with SKIE)
```swift
import shared
import Combine

class ProfileViewController: UIViewController {
    let viewModel = ProfileViewModel()
    var cancellables = Set<AnyCancellable>()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        // SKIE converts Flow to Swift's async sequence
        Task {
            for await state in viewModel.uiState {
                updateUI(state: state)
            }
        }
    }
}

// In SwiftUI
struct ProfileView: View {
    @StateFlow var state: ProfileUiState
    
    init(viewModel: ProfileViewModel) {
        _state = StateFlow(viewModel.uiState)
    }
    
    var body: some View {
        switch state {
        case is ProfileUiState.Loading:
            ProgressView()
        case let success as ProfileUiState.Success:
            ProfileContent(user: success.user)
        case let error as ProfileUiState.Error:
            Text(error.message)
        }
    }
}
```