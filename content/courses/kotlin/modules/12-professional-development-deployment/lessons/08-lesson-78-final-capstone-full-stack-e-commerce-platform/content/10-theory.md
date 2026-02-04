---
type: "THEORY"
title: "Client: API Client and Repository"
---

### Ktor HttpClient (API Client)

```kotlin
// composeApp/src/commonMain/kotlin/com/taskflow/app/data/remote/TaskFlowApi.kt
package com.taskflow.app.data.remote

import com.taskflow.shared.dto.ApiResponse
import com.taskflow.shared.dto.AuthResponse
import com.taskflow.shared.dto.CreateTaskRequest
import com.taskflow.shared.dto.LoginRequest
import com.taskflow.shared.dto.RegisterRequest
import com.taskflow.shared.dto.UpdateTaskRequest
import com.taskflow.shared.model.Task
import io.ktor.client.HttpClient
import io.ktor.client.call.body
import io.ktor.client.request.bearerAuth
import io.ktor.client.request.delete
import io.ktor.client.request.get
import io.ktor.client.request.post
import io.ktor.client.request.put
import io.ktor.client.request.setBody
import io.ktor.http.ContentType
import io.ktor.http.contentType

class TaskFlowApi(
    private val client: HttpClient,
    private val baseUrl: String = "http://localhost:8080"
) {
    private var token: String? = null

    fun setToken(jwt: String) {
        token = jwt
    }

    suspend fun register(request: RegisterRequest): AuthResponse {
        val response: ApiResponse<AuthResponse> = client.post("$baseUrl/api/auth/register") {
            contentType(ContentType.Application.Json)
            setBody(request)
        }.body()
        return response.data!!
    }

    suspend fun login(request: LoginRequest): AuthResponse {
        val response: ApiResponse<AuthResponse> = client.post("$baseUrl/api/auth/login") {
            contentType(ContentType.Application.Json)
            setBody(request)
        }.body()
        return response.data!!
    }

    suspend fun getAllTasks(): List<Task> {
        val response: ApiResponse<List<Task>> = client.get("$baseUrl/api/tasks") {
            bearerAuth(token!!)
        }.body()
        return response.data ?: emptyList()
    }

    suspend fun createTask(request: CreateTaskRequest): Task {
        val response: ApiResponse<Task> = client.post("$baseUrl/api/tasks") {
            bearerAuth(token!!)
            contentType(ContentType.Application.Json)
            setBody(request)
        }.body()
        return response.data!!
    }

    suspend fun updateTask(taskId: String, request: UpdateTaskRequest): Task {
        val response: ApiResponse<Task> = client.put("$baseUrl/api/tasks/$taskId") {
            bearerAuth(token!!)
            contentType(ContentType.Application.Json)
            setBody(request)
        }.body()
        return response.data!!
    }

    suspend fun deleteTask(taskId: String) {
        client.delete("$baseUrl/api/tasks/$taskId") {
            bearerAuth(token!!)
        }
    }
}
```

### TaskRepository (Offline-First)

The repository exposes reactive `StateFlow` to the UI layer. It reads from the local cache first, then syncs with the server.

```kotlin
// composeApp/src/commonMain/kotlin/com/taskflow/app/data/repository/TaskRepository.kt
package com.taskflow.app.data.repository

import com.taskflow.app.data.remote.TaskFlowApi
import com.taskflow.shared.dto.CreateTaskRequest
import com.taskflow.shared.dto.UpdateTaskRequest
import com.taskflow.shared.model.Task
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch

class TaskRepository(
    private val api: TaskFlowApi,
    private val syncManager: SyncManager,
    private val scope: CoroutineScope = CoroutineScope(Dispatchers.Default)
) {
    private val _tasks = MutableStateFlow<List<Task>>(emptyList())
    val tasks: StateFlow<List<Task>> = _tasks.asStateFlow()

    private val _isLoading = MutableStateFlow(false)
    val isLoading: StateFlow<Boolean> = _isLoading.asStateFlow()

    fun loadTasks() {
        // Read from cache immediately
        _tasks.value = syncManager.getAllTasks()

        // Then sync with server
        scope.launch {
            _isLoading.value = true
            try {
                val remoteTasks = api.getAllTasks()
                for (task in remoteTasks) {
                    syncManager.cacheTask(task)
                }
                _tasks.value = syncManager.getAllTasks()
            } catch (_: Exception) {
                // Offline -- cached data already displayed
            } finally {
                _isLoading.value = false
            }
        }
    }

    fun createTask(request: CreateTaskRequest) {
        scope.launch {
            try {
                val task = api.createTask(request)
                syncManager.cacheTask(task)
                _tasks.value = syncManager.getAllTasks()
            } catch (_: Exception) {
                // TODO: queue for offline sync
            }
        }
    }

    fun updateTask(taskId: String, request: UpdateTaskRequest) {
        scope.launch {
            try {
                val task = api.updateTask(taskId, request)
                syncManager.cacheTask(task)
                _tasks.value = syncManager.getAllTasks()
            } catch (_: Exception) {
                // TODO: queue for offline sync
            }
        }
    }

    fun deleteTask(taskId: String) {
        scope.launch {
            try {
                api.deleteTask(taskId)
                _tasks.value = _tasks.value.filter { it.id != taskId }
            } catch (_: Exception) {
                // TODO: queue for offline sync
            }
        }
    }
}
```

### Koin Module (Client)

```kotlin
// composeApp/src/commonMain/kotlin/com/taskflow/app/di/AppModule.kt
package com.taskflow.app.di

import com.taskflow.app.data.remote.TaskFlowApi
import com.taskflow.app.data.repository.SyncManager
import com.taskflow.app.data.repository.TaskRepository
import com.taskflow.app.viewmodel.AuthViewModel
import com.taskflow.app.viewmodel.TaskViewModel
import io.ktor.client.HttpClient
import io.ktor.client.plugins.contentnegotiation.ContentNegotiation
import io.ktor.serialization.kotlinx.json.json
import kotlinx.serialization.json.Json
import org.koin.dsl.module

val appModule = module {
    single {
        HttpClient {
            install(ContentNegotiation) {
                json(Json {
                    ignoreUnknownKeys = true
                    isLenient = true
                })
            }
        }
    }
    single { TaskFlowApi(get()) }
    single { SyncManager(get(), get()) }
    single { TaskRepository(get(), get()) }
    factory { AuthViewModel(get()) }
    factory { TaskViewModel(get()) }
}
```

---

