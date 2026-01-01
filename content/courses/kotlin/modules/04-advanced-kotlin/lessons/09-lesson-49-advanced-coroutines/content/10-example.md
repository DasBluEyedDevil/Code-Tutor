---
type: "EXAMPLE"
title: "SharedFlow for Events"
---

SharedFlow broadcasts one-time events to all collectors without replay by default. Use sealed classes to define event types, and emit() to send events. Events like navigation, snackbars, and loading indicators are perfect use cases.

```kotlin
import kotlinx.coroutines.*
import kotlinx.coroutines.flow.*

// Event types for one-time UI events
sealed class UiEvent {
    data class ShowSnackbar(val message: String) : UiEvent()
    data class Navigate(val route: String) : UiEvent()
    data object ShowLoading : UiEvent()
    data object HideLoading : UiEvent()
}

class EventViewModel {
    // SharedFlow for events - no replay, events are one-time
    private val _events = MutableSharedFlow<UiEvent>()
    val events: SharedFlow<UiEvent> = _events.asSharedFlow()
    
    suspend fun login(username: String, password: String) {
        _events.emit(UiEvent.ShowLoading)
        
        try {
            delay(1000) // Simulate network call
            
            if (username == "admin" && password == "123") {
                _events.emit(UiEvent.HideLoading)
                _events.emit(UiEvent.Navigate("/dashboard"))
            } else {
                _events.emit(UiEvent.HideLoading)
                _events.emit(UiEvent.ShowSnackbar("Invalid credentials"))
            }
        } catch (e: Exception) {
            _events.emit(UiEvent.HideLoading)
            _events.emit(UiEvent.ShowSnackbar("Network error: ${e.message}"))
        }
    }
}

fun main() = runBlocking {
    val viewModel = EventViewModel()
    
    // Event collector (simulating UI event handler)
    val job = launch {
        viewModel.events.collect { event ->
            when (event) {
                is UiEvent.ShowSnackbar -> println("SNACKBAR: ${event.message}")
                is UiEvent.Navigate -> println("NAVIGATE TO: ${event.route}")
                UiEvent.ShowLoading -> println("SHOW LOADING SPINNER")
                UiEvent.HideLoading -> println("HIDE LOADING SPINNER")
            }
        }
    }
    
    delay(100)
    println("--- Attempting login ---")
    viewModel.login("admin", "123")
    
    delay(1500)
    println("--- Failed login attempt ---")
    viewModel.login("user", "wrong")
    
    delay(1500)
    job.cancel()
}
```
