---
type: "EXAMPLE"
title: "Migrating Koin Annotations to KSP"
---


Koin annotations use KSP for dependency injection:



```kotlin
// build.gradle.kts with KSP for Koin
plugins {
    id("com.google.devtools.ksp") version "2.0.21-1.0.28"
}

dependencies {
    implementation("io.insert-koin:koin-core:4.0.0")
    implementation("io.insert-koin:koin-annotations:1.4.0")
    ksp("io.insert-koin:koin-ksp-compiler:1.4.0")
}

// Enable generated code
kotlin {
    sourceSets.main {
        kotlin.srcDir("build/generated/ksp/main/kotlin")
    }
}

// Usage with annotations:
@Module
@ComponentScan("com.example.app")
class AppModule

@Single
class UserRepository(
    private val database: Database
) {
    fun getUsers(): List<User> = database.users()
}

@Factory
class GetUsersUseCase(
    private val repository: UserRepository
) {
    operator fun invoke(): List<User> = repository.getUsers()
}
```
