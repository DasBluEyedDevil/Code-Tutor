---
type: "THEORY"
title: "Without SKIE: Manual Wrapper"
---

If not using SKIE, create a wrapper:

```kotlin
// In commonMain
class FlowWrapper<T>(private val flow: Flow<T>) {
    fun collect(
        onEach: (T) -> Unit,
        onCompletion: (Throwable?) -> Unit
    ): Cancellable {
        val scope = CoroutineScope(Dispatchers.Main + SupervisorJob())
        
        scope.launch {
            try {
                flow.collect { value ->
                    onEach(value)
                }
                onCompletion(null)
            } catch (e: Throwable) {
                onCompletion(e)
            }
        }
        
        return object : Cancellable {
            override fun cancel() {
                scope.cancel()
            }
        }
    }
}

interface Cancellable {
    fun cancel()
}

// Expose wrapped version
class ProfileViewModel {
    fun observeUiState(): FlowWrapper<ProfileUiState> {
        return FlowWrapper(uiState)
    }
}
```

### Swift Usage (without SKIE)
```swift
class ProfileViewController: UIViewController {
    let viewModel = ProfileViewModel()
    var cancellable: Cancellable?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        cancellable = viewModel.observeUiState().collect(
            onEach: { [weak self] state in
                self?.updateUI(state: state)
            },
            onCompletion: { error in
                if let error = error {
                    print("Error: \(error)")
                }
            }
        )
    }
    
    override func viewWillDisappear(_ animated: Bool) {
        super.viewWillDisappear(animated)
        cancellable?.cancel()
    }
}
```