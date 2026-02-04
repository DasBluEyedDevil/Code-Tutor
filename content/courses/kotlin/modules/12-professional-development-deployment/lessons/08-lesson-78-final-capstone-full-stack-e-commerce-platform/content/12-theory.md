---
type: "THEORY"
title: "Compose Multiplatform UI: Task Screens and App Entry"
---

### TaskListScreen

```kotlin
// composeApp/src/commonMain/kotlin/com/taskflow/app/ui/screen/TaskListScreen.kt
package com.taskflow.app.ui.screen

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.material.icons.filled.Delete
import androidx.compose.material3.Card
import androidx.compose.material3.CardDefaults
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.FloatingActionButton
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.material3.TopAppBar
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import com.taskflow.app.ui.component.PriorityChip
import com.taskflow.app.viewmodel.TaskViewModel
import com.taskflow.shared.model.Task
import com.taskflow.shared.model.TaskStatus

@Composable
fun TaskListScreen(
    taskViewModel: TaskViewModel,
    onAddTask: () -> Unit,
    onLogout: () -> Unit
) {
    val tasks by taskViewModel.tasks.collectAsState()
    val isLoading by taskViewModel.isLoading.collectAsState()

    LaunchedEffect(Unit) {
        taskViewModel.loadTasks()
    }

    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text("TaskFlow") },
                actions = {
                    TextButton(onClick = onLogout) {
                        Text("Logout")
                    }
                }
            )
        },
        floatingActionButton = {
            FloatingActionButton(onClick = onAddTask) {
                Icon(Icons.Default.Add, contentDescription = "Add Task")
            }
        }
    ) { paddingValues ->
        Box(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
        ) {
            if (isLoading && tasks.isEmpty()) {
                CircularProgressIndicator(modifier = Modifier.align(Alignment.Center))
            } else if (tasks.isEmpty()) {
                Text(
                    text = "No tasks yet. Tap + to create one.",
                    modifier = Modifier.align(Alignment.Center),
                    style = MaterialTheme.typography.bodyLarge
                )
            } else {
                LazyColumn(
                    modifier = Modifier.fillMaxSize().padding(16.dp),
                    verticalArrangement = Arrangement.spacedBy(8.dp)
                ) {
                    items(tasks, key = { it.id }) { task ->
                        TaskCard(
                            task = task,
                            onStatusChange = { newStatus ->
                                taskViewModel.updateTaskStatus(task.id, newStatus)
                            },
                            onDelete = { taskViewModel.deleteTask(task.id) }
                        )
                    }
                }
            }
        }
    }
}

@Composable
fun TaskCard(
    task: Task,
    onStatusChange: (TaskStatus) -> Unit,
    onDelete: () -> Unit
) {
    Card(
        modifier = Modifier.fillMaxWidth(),
        elevation = CardDefaults.cardElevation(defaultElevation = 2.dp)
    ) {
        Column(modifier = Modifier.padding(16.dp)) {
            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.SpaceBetween,
                verticalAlignment = Alignment.CenterVertically
            ) {
                Text(
                    text = task.title,
                    style = MaterialTheme.typography.titleMedium,
                    modifier = Modifier.weight(1f)
                )
                Row {
                    PriorityChip(priority = task.priority)
                    Spacer(modifier = Modifier.width(8.dp))
                    IconButton(onClick = onDelete) {
                        Icon(Icons.Default.Delete, contentDescription = "Delete")
                    }
                }
            }

            if (task.description.isNotBlank()) {
                Spacer(modifier = Modifier.height(4.dp))
                Text(
                    text = task.description,
                    style = MaterialTheme.typography.bodyMedium,
                    color = MaterialTheme.colorScheme.onSurfaceVariant
                )
            }

            Spacer(modifier = Modifier.height(8.dp))

            Row(horizontalArrangement = Arrangement.spacedBy(8.dp)) {
                TaskStatus.entries.forEach { status ->
                    TextButton(
                        onClick = { onStatusChange(status) },
                        enabled = task.status != status
                    ) {
                        Text(
                            text = status.name.replace("_", " "),
                            style = MaterialTheme.typography.labelSmall
                        )
                    }
                }
            }
        }
    }
}
```

### PriorityChip Component

```kotlin
// composeApp/src/commonMain/kotlin/com/taskflow/app/ui/component/PriorityChip.kt
package com.taskflow.app.ui.component

import androidx.compose.foundation.layout.padding
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.unit.dp
import com.taskflow.shared.model.Priority

@Composable
fun PriorityChip(priority: Priority) {
    val color = when (priority) {
        Priority.URGENT -> Color(0xFFD32F2F)
        Priority.HIGH -> Color(0xFFF57C00)
        Priority.MEDIUM -> Color(0xFF1976D2)
        Priority.LOW -> Color(0xFF388E3C)
    }

    Surface(
        color = color.copy(alpha = 0.12f),
        shape = MaterialTheme.shapes.small
    ) {
        Text(
            text = priority.name,
            color = color,
            style = MaterialTheme.typography.labelSmall,
            modifier = Modifier.padding(horizontal = 8.dp, vertical = 4.dp)
        )
    }
}
```

### App Entry Point and Navigation

```kotlin
// composeApp/src/commonMain/kotlin/com/taskflow/app/App.kt
package com.taskflow.app

import androidx.compose.material3.MaterialTheme
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import com.taskflow.app.ui.screen.LoginScreen
import com.taskflow.app.ui.screen.TaskListScreen
import com.taskflow.app.viewmodel.AuthViewModel
import com.taskflow.app.viewmodel.TaskViewModel
import org.koin.compose.koinInject

@Composable
fun App() {
    val authViewModel: AuthViewModel = koinInject()
    val taskViewModel: TaskViewModel = koinInject()
    var isLoggedIn by remember { mutableStateOf(false) }

    MaterialTheme {
        if (isLoggedIn) {
            TaskListScreen(
                taskViewModel = taskViewModel,
                onAddTask = { /* Navigate to create task screen */ },
                onLogout = {
                    authViewModel.logout()
                    isLoggedIn = false
                }
            )
        } else {
            LoginScreen(
                authViewModel = authViewModel,
                onLoginSuccess = { isLoggedIn = true }
            )
        }
    }
}
```

### Desktop Main

```kotlin
// composeApp/src/desktopMain/kotlin/com/taskflow/app/Main.kt
package com.taskflow.app

import androidx.compose.ui.window.Window
import androidx.compose.ui.window.application
import com.taskflow.app.di.appModule
import org.koin.core.context.startKoin

fun main() {
    startKoin {
        modules(appModule)
    }

    application {
        Window(
            onCloseRequest = ::exitApplication,
            title = "TaskFlow"
        ) {
            App()
        }
    }
}
```

---

