---
type: "THEORY"
title: "Setting Up Testing Dependencies"
---

### build.gradle.kts Configuration

```kotlin
kotlin {
    sourceSets {
        val commonMain by getting {
            dependencies {
                // Production dependencies
            }
        }
        
        val commonTest by getting {
            dependencies {
                // kotlin.test - multiplatform test framework
                implementation(kotlin("test"))
                
                // Coroutines test utilities
                implementation("org.jetbrains.kotlinx:kotlinx-coroutines-test:1.8.0")
                
                // Turbine for Flow testing
                implementation("app.cash.turbine:turbine:1.1.0")
            }
        }
    }
}
```

### kotlin.test Features

```kotlin
import kotlin.test.*

class MyTest {
    @BeforeTest
    fun setup() { /* Runs before each test */ }
    
    @AfterTest  
    fun teardown() { /* Runs after each test */ }
    
    @Test
    fun `descriptive test name`() {
        // Test body
    }
    
    @Ignore
    @Test
    fun `skipped test`() { /* Not run */ }
}
```