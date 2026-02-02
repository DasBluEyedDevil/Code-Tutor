---
type: "THEORY"
title: "Pattern 1: Expect/Actual Module Declaration"
---

Use expect/actual for the platform module itself:

```kotlin
// commonMain/kotlin/di/PlatformModule.kt
import org.koin.core.module.Module

expect val platformModule: Module
```

```kotlin
// androidMain/kotlin/di/PlatformModule.android.kt
import org.koin.dsl.module

actual val platformModule = module {
    single { AndroidSqliteDriver(...) }
    single { AppDatabase(get()) }
    single<PlatformContext> { get<Context>() }
}
```

```kotlin
// iosMain/kotlin/di/PlatformModule.ios.kt
import org.koin.dsl.module

actual val platformModule = module {
    single { NativeSqliteDriver(...) }
    single { AppDatabase(get()) }
    single<PlatformContext> { IOSContext() }
}
```

Now `platformModule` is available in common code!