---
type: "WARNING"
title: "Annotation Gotchas"
---

### ❌ Forgetting to run KSP

After changing annotated classes, rebuild the project:
```bash
./gradlew clean kspCommonMainKotlinMetadata
```

### ❌ Missing @Bind for interfaces

```kotlin
// ❌ Won't be found when requesting NotesRepository
@Single
class NotesRepositoryImpl : NotesRepository

// ✅ Correct
@Single
@Bind(NotesRepository::class)
class NotesRepositoryImpl : NotesRepository
```

### ❌ Incorrect ComponentScan path

```kotlin
// Make sure the package path is correct
@Module
@ComponentScan("com.example.app.wrong.path")  // Nothing found!
class AppModule
```

### ❌ Mixing annotation and DSL for same type

Don't define the same type in both annotations and DSL modules - it leads to duplicates.