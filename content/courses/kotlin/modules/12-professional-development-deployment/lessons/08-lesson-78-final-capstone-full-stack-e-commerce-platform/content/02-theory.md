---
type: "THEORY"
title: "Architecture Overview"
---

### Project Structure

```
taskflow/
├── build.gradle.kts              # Root build file
├── settings.gradle.kts           # Module declarations
├── gradle/
│   └── libs.versions.toml        # Version catalog (single source of truth)
├── shared/
│   └── src/
│       └── commonMain/
│           └── kotlin/com/taskflow/shared/
│               ├── model/
│               │   ├── Task.kt
│               │   ├── User.kt
│               │   └── Priority.kt
│               └── dto/
│                   ├── TaskRequest.kt
│                   ├── TaskResponse.kt
│                   ├── AuthRequest.kt
│                   └── AuthResponse.kt
├── server/
│   └── src/
│       └── main/
│           ├── kotlin/com/taskflow/server/
│           │   ├── Application.kt
│           │   ├── plugins/
│           │   │   ├── Routing.kt
│           │   │   ├── Serialization.kt
│           │   │   ├── Security.kt
│           │   │   └── Database.kt
│           │   ├── routes/
│           │   │   ├── AuthRoutes.kt
│           │   │   └── TaskRoutes.kt
│           │   ├── db/
│           │   │   ├── tables/
│           │   │   │   ├── Users.kt
│           │   │   │   └── Tasks.kt
│           │   │   └── dao/
│           │   │       ├── UserDao.kt
│           │   │       └── TaskDao.kt
│           │   └── di/
│           │       └── ServerModule.kt
│           └── resources/
│               └── application.conf
└── composeApp/
    └── src/
        ├── commonMain/
        │   └── kotlin/com/taskflow/app/
        │       ├── App.kt
        │       ├── ui/
        │       │   ├── screen/
        │       │   │   ├── LoginScreen.kt
        │       │   │   ├── TaskListScreen.kt
        │       │   │   └── TaskDetailScreen.kt
        │       │   ├── component/
        │       │   │   ├── TaskCard.kt
        │       │   │   └── PriorityChip.kt
        │       │   └── theme/
        │       │       └── Theme.kt
        │       ├── viewmodel/
        │       │   ├── AuthViewModel.kt
        │       │   └── TaskViewModel.kt
        │       ├── data/
        │       │   ├── remote/
        │       │   │   └── TaskFlowApi.kt
        │       │   ├── local/
        │       │   │   └── TaskFlowDatabase.sq
        │       │   └── repository/
        │       │       └── TaskRepository.kt
        │       └── di/
        │           └── AppModule.kt
        ├── androidMain/
        │   └── kotlin/com/taskflow/app/
        │       ├── MainActivity.kt
        │       └── DatabaseDriverFactory.kt
        └── desktopMain/
            └── kotlin/com/taskflow/app/
                ├── Main.kt
                └── DatabaseDriverFactory.kt
```

### Data Flow

```
User Action (Compose UI)
        │
        ▼
   ViewModel (coroutines + StateFlow)
        │
        ▼
   TaskRepository (offline-first logic)
        │
   ┌────┴────┐
   ▼         ▼
SQLDelight   Ktor HttpClient
(local)      (remote → server/)
                  │
                  ▼
            Ktor Server
                  │
                  ▼
         Exposed + H2 Database
```

**Offline-first strategy**: The client reads from SQLDelight first (instant UI), then syncs with the server in the background. If the network is unavailable, the local cache serves the UI. When connectivity returns, pending changes are pushed to the server.

---

