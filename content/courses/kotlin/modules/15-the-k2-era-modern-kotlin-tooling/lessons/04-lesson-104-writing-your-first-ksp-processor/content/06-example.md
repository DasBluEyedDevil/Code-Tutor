---
type: "EXAMPLE"
title: "Creating the Processor"
---


The core processor implementation:



```kotlin
// processor/src/main/kotlin/com/example/AutoBuilderProcessor.kt
package com.example

import com.google.devtools.ksp.processing.*
import com.google.devtools.ksp.symbol.*
import com.google.devtools.ksp.validate

class AutoBuilderProcessor(
    private val codeGenerator: CodeGenerator,
    private val logger: KSPLogger
) : SymbolProcessor {

    override fun process(resolver: Resolver): List<KSAnnotated> {
        // Find all classes annotated with @AutoBuilder
        val symbols = resolver.getSymbolsWithAnnotation(
            AutoBuilder::class.qualifiedName!!
        )
        
        // Filter to class declarations that are valid
        val (valid, invalid) = symbols
            .filterIsInstance<KSClassDeclaration>()
            .partition { it.validate() }
        
        // Process valid symbols
        valid.forEach { classDecl ->
            logger.info("Processing: ${classDecl.simpleName.asString()}")
            generateBuilder(classDecl)
        }
        
        // Return invalid symbols for reprocessing in next round
        return invalid
    }
    
    private fun generateBuilder(classDecl: KSClassDeclaration) {
        val className = classDecl.simpleName.asString()
        val packageName = classDecl.packageName.asString()
        val properties = classDecl.getAllProperties().toList()
        
        // Validate: must be a data class
        if (!classDecl.modifiers.contains(Modifier.DATA)) {
            logger.error("@AutoBuilder can only be applied to data classes", classDecl)
            return
        }
        
        // Generate builder code using KotlinPoet (next example)
        generateBuilderWithKotlinPoet(classDecl, packageName, className, properties)
    }
}
```
