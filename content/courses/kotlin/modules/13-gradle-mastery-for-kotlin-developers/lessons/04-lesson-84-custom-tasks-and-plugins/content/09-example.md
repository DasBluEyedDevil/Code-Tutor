---
type: "EXAMPLE"
title: "Practical Task: Generate API Client"
---


A real-world example of code generation:



```kotlin
tasks.register("generateApiClient") {
    group = "codegen"
    description = "Generates API client from OpenAPI spec"
    
    val specFile = file("api/openapi.yaml")
    val outputDir = layout.buildDirectory.dir("generated/api")
    
    inputs.file(specFile)
    outputs.dir(outputDir)
    
    doLast {
        val spec = specFile.readText()
        val output = outputDir.get().asFile
        output.mkdirs()
        
        // Parse spec and generate client code
        println("Generating API client from ${specFile.name}")
        
        // Add generated sources to compilation
        val generatedFile = File(output, "ApiClient.kt")
        generatedFile.writeText("""
            package com.example.api
            
            class ApiClient {
                // Generated from OpenAPI spec
            }
        """.trimIndent())
    }
}

// Add generated sources to source sets
kotlin {
    sourceSets {
        main {
            kotlin.srcDir(layout.buildDirectory.dir("generated/api"))
        }
    }
}

tasks.named("compileKotlin") {
    dependsOn("generateApiClient")
}
```
