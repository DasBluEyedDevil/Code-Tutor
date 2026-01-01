---
type: "EXAMPLE"
title: "Testing Your Processor"
---


Use compile-testing to test your processor:



```kotlin
// processor/build.gradle.kts
dependencies {
    testImplementation("com.github.tschuchortdev:kotlin-compile-testing-ksp:1.6.0")
    testImplementation(kotlin("test"))
}

// processor/src/test/kotlin/AutoBuilderProcessorTest.kt
package com.example

import com.tschuchort.compiletesting.*
import org.junit.jupiter.api.Test
import kotlin.test.*

class AutoBuilderProcessorTest {
    
    @Test
    fun `generates builder for data class`() {
        val source = SourceFile.kotlin(
            "User.kt",
            """
            package com.example
            
            @AutoBuilder
            data class User(val id: Long, val name: String)
            """.trimIndent()
        )
        
        val result = KotlinCompilation().apply {
            sources = listOf(source)
            symbolProcessorProviders = listOf(AutoBuilderProcessorProvider())
            inheritClassPath = true
        }.compile()
        
        assertEquals(KotlinCompilation.ExitCode.OK, result.exitCode)
        
        // Check generated file exists
        val generatedFile = result.kspSourcesDir
            .walkTopDown()
            .find { it.name == "UserBuilder.kt" }
        
        assertNotNull(generatedFile)
        assertTrue(generatedFile.readText().contains("class UserBuilder"))
    }
    
    @Test
    fun `fails for non-data class`() {
        val source = SourceFile.kotlin(
            "RegularClass.kt",
            """
            package com.example
            
            @AutoBuilder
            class RegularClass(val id: Long)  // Not a data class
            """.trimIndent()
        )
        
        val result = KotlinCompilation().apply {
            sources = listOf(source)
            symbolProcessorProviders = listOf(AutoBuilderProcessorProvider())
            inheritClassPath = true
        }.compile()
        
        // Should have error message
        assertTrue(result.messages.contains("data classes"))
    }
}
```
