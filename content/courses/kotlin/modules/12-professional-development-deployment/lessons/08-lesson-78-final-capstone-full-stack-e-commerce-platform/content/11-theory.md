---
type: "THEORY"
title: "Compose Multiplatform UI: ViewModels and Screens"
---

### AuthViewModel

```kotlin
// composeApp/src/commonMain/kotlin/com/taskflow/app/viewmodel/AuthViewModel.kt
package com.taskflow.app.viewmodel

import com.taskflow.app.data.remote.TaskFlowApi
import com.taskflow.shared.dto.LoginRequest
import com.taskflow.shared.dto.RegisterRequest
import com.taskflow.shared.model.User
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch

class AuthViewModel(
    private val api: TaskFlowApi,
    private val scope: CoroutineScope = CoroutineScope(Dispatchers.Default)
) {
    private val _user = MutableStateFlow<User?>(null)
    val user: StateFlow<User?> = _user.asStateFlow()

    private val _error = MutableStateFlow<String?>(null)
    val error: StateFlow<String?> = _error.asStateFlow()

    private val _isLoading = MutableStateFlow(false)
    val isLoading: StateFlow<Boolean> = _isLoading.asStateFlow()

    val isLoggedIn: Boolean get() = _user.value != null

    fun login(email: String, password: String) {
        scope.launch {
            _isLoading.value = true
            _error.value = null
            try {
                val response = api.login(LoginRequest(email, password))
                api.setToken(response.token)
                _user.value = response.user
            } catch (e: Exception) {
                _error.value = "Login failed: ${e.message}"
            } finally {
                _isLoading.value = false
            }
        }
    }

    fun register(email: String, password: String, displayName: String) {
        scope.launch {
            _isLoading.value = true
            _error.value = null
            try {
                val response = api.register(RegisterRequest(email, password, displayName))
                api.setToken(response.token)
                _user.value = response.user
            } catch (e: Exception) {
                _error.value = "Registration failed: ${e.message}"
            } finally {
                _isLoading.value = false
            }
        }
    }

    fun logout() {
        _user.value = null
    }
}
```

### TaskViewModel

```kotlin
// composeApp/src/commonMain/kotlin/com/taskflow/app/viewmodel/TaskViewModel.kt
package com.taskflow.app.viewmodel

import com.taskflow.app.data.repository.TaskRepository
import com.taskflow.shared.dto.CreateTaskRequest
import com.taskflow.shared.dto.UpdateTaskRequest
import com.taskflow.shared.model.Priority
import com.taskflow.shared.model.Task
import com.taskflow.shared.model.TaskStatus
import kotlinx.coroutines.flow.StateFlow

class TaskViewModel(private val repository: TaskRepository) {
    val tasks: StateFlow<List<Task>> = repository.tasks
    val isLoading: StateFlow<Boolean> = repository.isLoading

    fun loadTasks() {
        repository.loadTasks()
    }

    fun createTask(title: String, description: String, priority: Priority, category: String) {
        repository.createTask(
            CreateTaskRequest(
                title = title,
                description = description,
                priority = priority,
                category = category
            )
        )
    }

    fun updateTaskStatus(taskId: String, status: TaskStatus) {
        repository.updateTask(taskId, UpdateTaskRequest(status = status))
    }

    fun deleteTask(taskId: String) {
        repository.deleteTask(taskId)
    }
}
```

### LoginScreen

```kotlin
// composeApp/src/commonMain/kotlin/com/taskflow/app/ui/screen/LoginScreen.kt
package com.taskflow.app.ui.screen

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Button
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.compose.ui.unit.dp
import com.taskflow.app.viewmodel.AuthViewModel

@Composable
fun LoginScreen(
    authViewModel: AuthViewModel,
    onLoginSuccess: () -> Unit
) {
    var email by remember { mutableStateOf("") }
    var password by remember { mutableStateOf("") }
    var displayName by remember { mutableStateOf("") }
    var isRegisterMode by remember { mutableStateOf(false) }

    val isLoading by authViewModel.isLoading.collectAsState()
    val error by authViewModel.error.collectAsState()
    val user by authViewModel.user.collectAsState()

    if (user != null) {
        onLoginSuccess()
        return
    }

    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(32.dp),
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        Text(
            text = "TaskFlow",
            style = MaterialTheme.typography.headlineLarge,
            color = MaterialTheme.colorScheme.primary
        )

        Spacer(modifier = Modifier.height(32.dp))

        if (isRegisterMode) {
            OutlinedTextField(
                value = displayName,
                onValueChange = { displayName = it },
                label = { Text("Display Name") },
                modifier = Modifier.fillMaxWidth(),
                singleLine = true
            )
            Spacer(modifier = Modifier.height(8.dp))
        }

        OutlinedTextField(
            value = email,
            onValueChange = { email = it },
            label = { Text("Email") },
            modifier = Modifier.fillMaxWidth(),
            singleLine = true
        )

        Spacer(modifier = Modifier.height(8.dp))

        OutlinedTextField(
            value = password,
            onValueChange = { password = it },
            label = { Text("Password") },
            modifier = Modifier.fillMaxWidth(),
            singleLine = true,
            visualTransformation = PasswordVisualTransformation()
        )

        error?.let {
            Spacer(modifier = Modifier.height(8.dp))
            Text(text = it, color = MaterialTheme.colorScheme.error)
        }

        Spacer(modifier = Modifier.height(16.dp))

        if (isLoading) {
            CircularProgressIndicator()
        } else {
            Button(
                onClick = {
                    if (isRegisterMode) {
                        authViewModel.register(email, password, displayName)
                    } else {
                        authViewModel.login(email, password)
                    }
                },
                modifier = Modifier.fillMaxWidth()
            ) {
                Text(if (isRegisterMode) "Register" else "Sign In")
            }
        }

        Spacer(modifier = Modifier.height(8.dp))

        TextButton(onClick = { isRegisterMode = !isRegisterMode }) {
            Text(
                if (isRegisterMode) "Already have an account? Sign In"
                else "Don't have an account? Register"
            )
        }
    }
}
```

---

