---
type: "THEORY"
title: "Project Structure"
---


This is a Compose Multiplatform project structure that works on both Android and iOS:

---



```kotlin
composeApp/
├── src/
│   ├── commonMain/kotlin/com/example/taskmaster/  # Shared code
│   │   ├── data/
│   │   │   ├── local/
│   │   │   │   ├── TaskDatabase.kt
│   │   │   │   └── TaskEntity.kt
│   │   │   ├── repository/
│   │   │   │   └── TaskRepository.kt
│   │   │   └── model/
│   │   │       ├── Task.kt
│   │   │       ├── Category.kt
│   │   │       └── Priority.kt
│   │   ├── di/
│   │   │   └── AppModule.kt
│   │   ├── ui/
│   │   │   ├── theme/
│   │   │   │   ├── Color.kt
│   │   │   │   ├── Theme.kt
│   │   │   │   └── Type.kt
│   │   │   ├── components/
│   │   │   │   ├── TaskItem.kt
│   │   │   │   ├── CategoryChip.kt
│   │   │   │   └── PriorityBadge.kt
│   │   │   └── screens/
│   │   │       ├── HomeScreen.kt
│   │   │       ├── AddEditScreen.kt
│   │   │       └── StatisticsScreen.kt
│   │   └── App.kt  # Main shared composable
│   ├── androidMain/kotlin/  # Android-specific
│   │   └── MainActivity.kt
│   └── iosMain/kotlin/       # iOS-specific
│       └── MainViewController.kt
iosApp/   # iOS Xcode project
    └── iosApp.xcodeproj
```
