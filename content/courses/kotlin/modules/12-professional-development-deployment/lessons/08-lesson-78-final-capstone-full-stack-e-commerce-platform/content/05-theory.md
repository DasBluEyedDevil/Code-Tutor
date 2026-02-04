---
type: "THEORY"
title: "Shared Domain Layer"
---

The `shared/` module contains `@Serializable` data classes used by both the server and the client. Because these live in `commonMain`, they compile to JVM (server), Android, Desktop, and iOS targets.

### Domain Models

```kotlin
// shared/src/commonMain/kotlin/com/taskflow/shared/model/Priority.kt
package com.taskflow.shared.model

import kotlinx.serialization.Serializable

@Serializable
enum class Priority {
    LOW, MEDIUM, HIGH, URGENT
}
```

```kotlin
// shared/src/commonMain/kotlin/com/taskflow/shared/model/TaskStatus.kt
package com.taskflow.shared.model

import kotlinx.serialization.Serializable

@Serializable
enum class TaskStatus {
    TODO, IN_PROGRESS, DONE
}
```

```kotlin
// shared/src/commonMain/kotlin/com/taskflow/shared/model/Task.kt
package com.taskflow.shared.model

import kotlinx.serialization.Serializable

@Serializable
data class Task(
    val id: String,
    val title: String,
    val description: String = "",
    val priority: Priority = Priority.MEDIUM,
    val status: TaskStatus = TaskStatus.TODO,
    val category: String = "",
    val dueDate: String? = null,
    val createdAt: String = "",
    val updatedAt: String = "",
    val userId: String = ""
)
```

```kotlin
// shared/src/commonMain/kotlin/com/taskflow/shared/model/User.kt
package com.taskflow.shared.model

import kotlinx.serialization.Serializable

@Serializable
data class User(
    val id: String,
    val email: String,
    val displayName: String
)
```

### DTOs (Data Transfer Objects)

DTOs define the shape of API requests and responses. They separate wire format from domain models.

```kotlin
// shared/src/commonMain/kotlin/com/taskflow/shared/dto/AuthRequest.kt
package com.taskflow.shared.dto

import kotlinx.serialization.Serializable

@Serializable
data class RegisterRequest(
    val email: String,
    val password: String,
    val displayName: String
)

@Serializable
data class LoginRequest(
    val email: String,
    val password: String
)
```

```kotlin
// shared/src/commonMain/kotlin/com/taskflow/shared/dto/AuthResponse.kt
package com.taskflow.shared.dto

import com.taskflow.shared.model.User
import kotlinx.serialization.Serializable

@Serializable
data class AuthResponse(
    val token: String,
    val user: User
)
```

```kotlin
// shared/src/commonMain/kotlin/com/taskflow/shared/dto/TaskRequest.kt
package com.taskflow.shared.dto

import com.taskflow.shared.model.Priority
import com.taskflow.shared.model.TaskStatus
import kotlinx.serialization.Serializable

@Serializable
data class CreateTaskRequest(
    val title: String,
    val description: String = "",
    val priority: Priority = Priority.MEDIUM,
    val category: String = "",
    val dueDate: String? = null
)

@Serializable
data class UpdateTaskRequest(
    val title: String? = null,
    val description: String? = null,
    val priority: Priority? = null,
    val status: TaskStatus? = null,
    val category: String? = null,
    val dueDate: String? = null
)
```

```kotlin
// shared/src/commonMain/kotlin/com/taskflow/shared/dto/ApiResponse.kt
package com.taskflow.shared.dto

import kotlinx.serialization.Serializable

@Serializable
data class ApiResponse<T>(
    val success: Boolean,
    val data: T? = null,
    val message: String? = null
)
```

### Why Shared DTOs Matter

With shared DTOs, the server serializes a `Task` and the client deserializes the exact same class. No mapping code, no mismatched fields, no version drift between client and server. This is a core benefit of Kotlin Multiplatform.

---

