---
type: "EXAMPLE"
title: "Using on iOS"
---

Consume the shared ViewModel in SwiftUI or UIKit:

```kotlin
// ========== iosMain - Helper for iOS ==========
class IosNotesViewModel(
    private val viewModel: NotesViewModel
) {
    val state: CommonStateFlow<NotesState> = viewModel.state.wrap()
    
    fun addNote(title: String, content: String) = viewModel.addNote(title, content)
    fun deleteNote(noteId: String) = viewModel.deleteNote(noteId)
    fun dispose() = viewModel.onCleared()
}

// CommonStateFlow wrapper for iOS
class CommonStateFlow<T>(private val flow: StateFlow<T>) {
    fun getValue(): T = flow.value
    
    fun collect(onEach: (T) -> Unit): Cancellable {
        val job = CoroutineScope(Dispatchers.Main).launch {
            flow.collect { onEach(it) }
        }
        return object : Cancellable {
            override fun cancel() { job.cancel() }
        }
    }
}

interface Cancellable {
    fun cancel()
}

fun <T> StateFlow<T>.wrap(): CommonStateFlow<T> = CommonStateFlow(this)
```
