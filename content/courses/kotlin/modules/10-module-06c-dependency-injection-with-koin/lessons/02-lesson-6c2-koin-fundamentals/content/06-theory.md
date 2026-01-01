---
type: "THEORY"
title: "Qualifiers: Multiple Implementations"
---

What if you have two implementations of the same interface?

```kotlin
interface ApiClient {
    suspend fun fetch(url: String): String
}

class ProductionApiClient : ApiClient { ... }
class MockApiClient : ApiClient { ... }
```

### Using Named Qualifiers

```kotlin
import org.koin.core.qualifier.named

val apiModule = module {
    single<ApiClient>(named("production")) { ProductionApiClient() }
    single<ApiClient>(named("mock")) { MockApiClient() }
}

// Retrieving with qualifier
val productionClient = get<ApiClient>(named("production"))
val mockClient = get<ApiClient>(named("mock"))
```

### Using Enum Qualifiers (Cleaner)

```kotlin
enum class ApiType {
    PRODUCTION, MOCK
}

val apiModule = module {
    single<ApiClient>(named(ApiType.PRODUCTION)) { ProductionApiClient() }
    single<ApiClient>(named(ApiType.MOCK)) { MockApiClient() }
}
```

### Real-World Example: Multiple Dispatchers

```kotlin
import kotlinx.coroutines.Dispatchers
import org.koin.core.qualifier.named

enum class DispatcherType {
    IO, DEFAULT, MAIN
}

val dispatcherModule = module {
    single(named(DispatcherType.IO)) { Dispatchers.IO }
    single(named(DispatcherType.DEFAULT)) { Dispatchers.Default }
    single(named(DispatcherType.MAIN)) { Dispatchers.Main }
}

// Use in repository
class NotesRepositoryImpl(
    private val ioDispatcher: CoroutineDispatcher,
    private val database: AppDatabase
) : NotesRepository {
    // ...
}

val dataModule = module {
    single<NotesRepository> { 
        NotesRepositoryImpl(
            ioDispatcher = get(named(DispatcherType.IO)),
            database = get()
        )
    }
}
```