---
type: "EXAMPLE"
title: "Code Generation with KotlinPoet"
---


KotlinPoet makes generating Kotlin code easy:



```kotlin
// Continue from previous example
import com.squareup.kotlinpoet.*
import com.squareup.kotlinpoet.ksp.writeTo
import com.squareup.kotlinpoet.ksp.toTypeName

private fun generateBuilderWithKotlinPoet(
    classDecl: KSClassDeclaration,
    packageName: String,
    className: String,
    properties: List<KSPropertyDeclaration>
) {
    val builderClassName = "${className}Builder"
    
    // Create the builder class
    val builderClass = TypeSpec.classBuilder(builderClassName)
        .apply {
            // Add nullable properties
            properties.forEach { prop ->
                val propName = prop.simpleName.asString()
                val propType = prop.type.resolve().toTypeName()
                
                addProperty(
                    PropertySpec.builder(propName, propType.copy(nullable = true))
                        .mutable()
                        .initializer("null")
                        .build()
                )
            }
            
            // Add fluent setter methods
            properties.forEach { prop ->
                val propName = prop.simpleName.asString()
                val propType = prop.type.resolve().toTypeName()
                
                addFunction(
                    FunSpec.builder(propName)
                        .addParameter("value", propType)
                        .returns(ClassName(packageName, builderClassName))
                        .addStatement("this.%L = value", propName)
                        .addStatement("return this")
                        .build()
                )
            }
            
            // Add build() method
            val buildParams = properties.joinToString(", ") {
                "${it.simpleName.asString()} = ${it.simpleName.asString()}!!"
            }
            
            addFunction(
                FunSpec.builder("build")
                    .returns(ClassName(packageName, className))
                    .addStatement("return %L(%L)", className, buildParams)
                    .build()
            )
        }
        .build()
    
    // Create the file
    val fileSpec = FileSpec.builder(packageName, builderClassName)
        .addType(builderClass)
        .build()
    
    // Write to generated sources
    fileSpec.writeTo(
        codeGenerator,
        Dependencies(true, classDecl.containingFile!!)
    )
}
```
