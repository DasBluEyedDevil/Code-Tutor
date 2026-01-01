---
type: "THEORY"
title: "Core Annotations"
---

### @Module
Marks a class that groups related definitions:

```kotlin
@Module
class DataModule
```

### @Single
Creates a singleton instance:

```kotlin
@Single
class AppDatabase(...)

// With interface binding
@Single
@Bind(NotesRepository::class)
class NotesRepositoryImpl(...) : NotesRepository
```

### @Factory
Creates a new instance each time:

```kotlin
@Factory
class CreateNoteUseCase(private val repository: NotesRepository)
```

### @KoinViewModel
For ViewModels with lifecycle awareness:

```kotlin
@KoinViewModel
class NotesViewModel(private val repository: NotesRepository)
```

### @Named
For multiple implementations:

```kotlin
@Single
@Named("production")
class ProductionApiClient : ApiClient

@Single
@Named("mock")
class MockApiClient : ApiClient
```