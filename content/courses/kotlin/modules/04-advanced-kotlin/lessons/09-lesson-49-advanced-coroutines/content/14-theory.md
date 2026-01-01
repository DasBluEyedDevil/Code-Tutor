---
type: "THEORY"
title: "Exercises"
---


### Exercise 1: Temperature Monitor with Flow (Medium)

Create a temperature monitoring system using Flow.

**Requirements**:
- Generate random temperatures every second
- Filter temperatures above 30°C
- Calculate running average
- Emit alerts for high temperatures

**Solution**:


### Exercise 2: Download Manager with Channels (Hard)

Build a concurrent download manager using channels.

**Requirements**:
- Multiple download workers
- Task queue with channel
- Progress reporting
- Completion notification

**Solution**:


### Exercise 3: Real-Time Chat with StateFlow (Hard)

Create a simple chat system with StateFlow for state management.

**Requirements**:
- User state (online/offline)
- Message history
- Real-time updates
- Multiple observers

**Solution**:


---



```kotlin
import kotlinx.coroutines.*
import kotlinx.coroutines.flow.*

data class Message(val user: String, val text: String, val timestamp: Long)
data class ChatState(
    val users: Set<String>,
    val messages: List<Message>
)

class ChatRoom {
    private val _state = MutableStateFlow(ChatState(emptySet(), emptyList()))
    val state: StateFlow<ChatState> = _state

    fun userJoin(username: String) {
        _state.value = _state.value.copy(
            users = _state.value.users + username,
            messages = _state.value.messages + Message(
                "System",
                "$username joined",
                System.currentTimeMillis()
            )
        )
    }

    fun userLeave(username: String) {
        _state.value = _state.value.copy(
            users = _state.value.users - username,
            messages = _state.value.messages + Message(
                "System",
                "$username left",
                System.currentTimeMillis()
            )
        )
    }

    fun sendMessage(username: String, text: String) {
        _state.value = _state.value.copy(
            messages = _state.value.messages + Message(
                username,
                text,
                System.currentTimeMillis()
            )
        )
    }
}

fun main() = runBlocking {
    val chatRoom = ChatRoom()

    // Observer 1
    launch {
        chatRoom.state
            .map { it.users.size }
            .distinctUntilChanged()
            .collect { count ->
                println("👥 Users online: $count")
            }
    }

    // Observer 2
    launch {
        chatRoom.state
            .map { it.messages.lastOrNull() }
            .filterNotNull()
            .collect { msg ->
                println("💬 [${msg.user}]: ${msg.text}")
            }
    }

    delay(100)

    chatRoom.userJoin("Alice")
    delay(100)
    chatRoom.userJoin("Bob")
    delay(100)
    chatRoom.sendMessage("Alice", "Hello, Bob!")
    delay(100)
    chatRoom.sendMessage("Bob", "Hi, Alice!")
    delay(100)
    chatRoom.userLeave("Alice")

    delay(500)
}
```
