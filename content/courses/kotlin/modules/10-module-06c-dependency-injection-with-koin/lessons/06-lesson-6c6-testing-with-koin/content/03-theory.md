---
type: "THEORY"
title: "Testing with Koin: Setup"
---

### Test Dependencies

```toml
# gradle/libs.versions.toml
[libraries]
koin-test = { module = "io.insert-koin:koin-test", version.ref = "koin" }
koin-test-junit5 = { module = "io.insert-koin:koin-test-junit5", version.ref = "koin" }
```

```kotlin
// commonTest
commonTest.dependencies {
    implementation(libs.koin.test)
}
```

### Base Test Class

```kotlin
import org.koin.core.context.startKoin
import org.koin.core.context.stopKoin
import org.koin.test.KoinTest
import org.koin.test.inject
import kotlin.test.AfterTest
import kotlin.test.BeforeTest

abstract class BaseKoinTest : KoinTest {
    
    @BeforeTest
    fun setUp() {
        startKoin {
            modules(testModules())
        }
    }
    
    @AfterTest
    fun tearDown() {
        stopKoin()
    }
    
    abstract fun testModules(): List<Module>
}
```