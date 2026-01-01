---
type: "THEORY"
title: "KMP with Annotations"
---

### Common Module with Annotations

```kotlin
// commonMain/kotlin/di/CommonModule.kt
@Module
@ComponentScan("com.example.app")
class CommonModule

@Single
@Bind(NotesRepository::class)
class NotesRepositoryImpl(
    @Provided database: AppDatabase  // Platform-provided
) : NotesRepository

@KoinViewModel
class NotesViewModel(
    private val repository: NotesRepository
)
```

### Platform Module (Still DSL)

Platform modules often stay as DSL since they need platform-specific code:

```kotlin
// androidMain/kotlin/di/PlatformModule.kt
val platformModule = module {
    single {
        AndroidSqliteDriver(
            schema = AppDatabase.Schema,
            context = get(),
            name = "notes.db"
        )
    }
    single { AppDatabase(get()) }
}
```

### Combining Annotated and DSL Modules

```kotlin
fun initKoin(platformModule: Module) {
    startKoin {
        modules(
            platformModule,                    // DSL module
            CommonModuleModule().module        // Generated from annotations
        )
    }
}
```