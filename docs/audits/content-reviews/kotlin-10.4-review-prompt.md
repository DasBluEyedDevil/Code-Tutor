# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** The K2 Era - Modern Kotlin Tooling
- **Lesson:** Lesson 10.4: Writing Your First KSP Processor (ID: 10.4)
- **Difficulty:** advanced
- **Estimated Time:** 75 minutes

## Current Lesson Content

{
    "id":  "10.4",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 75 minutes\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Topic Introduction",
                                "content":  "\nKSP enables you to write your own code generators that run at compile time. This is powerful for reducing boilerplate, enforcing patterns, and generating type-safe code.\n\nIn this lesson, you\u0027ll learn:\n- KSP processor architecture\n- How to create a processor project\n- Processing annotations and generating code\n- Using KotlinPoet for code generation\n- Testing your processor\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "KSP Architecture",
                                "content":  "\n### How KSP Works\n\nKSP processes Kotlin source files and can generate new source files:\n\n```\nKotlin Source -\u003e KSP Processor -\u003e Generated Kotlin Source\n     |                |                    |\n  @AutoBuilder    Process symbols    UserBuilder.kt\n  data class     Generate code\n  User(...)\n```\n\n**Key Components**\n\n1. **SymbolProcessor** - Your processor implementation\n2. **SymbolProcessorProvider** - Factory for creating processors\n3. **Resolver** - Provides access to symbols (classes, functions, etc.)\n4. **CodeGenerator** - Creates output files\n5. **KSPLogger** - Logs messages and errors\n\n---\n\n"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Project Structure",
                                "content":  "\nA KSP processor typically uses a multi-module setup:\n\n",
                                "code":  "// Project structure:\n// my-project/\n// ├── annotations/          # Annotation definitions\n// │   ├── build.gradle.kts\n// │   └── src/main/kotlin/\n// │       └── AutoBuilder.kt\n// ├── processor/             # KSP processor\n// │   ├── build.gradle.kts\n// │   └── src/main/kotlin/\n// │       └── AutoBuilderProcessor.kt\n// └── app/                   # Application using the processor\n//     └── build.gradle.kts\n\n// annotations/build.gradle.kts\nplugins {\n    kotlin(\"jvm\")\n}\n\n// This module has no dependencies - just the annotation\n\n// processor/build.gradle.kts\nplugins {\n    kotlin(\"jvm\")\n}\n\ndependencies {\n    implementation(project(\":annotations\"))\n    implementation(\"com.google.devtools.ksp:symbol-processing-api:2.0.21-1.0.28\")\n    implementation(\"com.squareup:kotlinpoet:1.18.1\")\n    implementation(\"com.squareup:kotlinpoet-ksp:1.18.1\")\n}\n\n// app/build.gradle.kts\nplugins {\n    kotlin(\"jvm\")\n    id(\"com.google.devtools.ksp\") version \"2.0.21-1.0.28\"\n}\n\ndependencies {\n    implementation(project(\":annotations\"))\n    ksp(project(\":processor\"))\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Defining the Annotation",
                                "content":  "\nStart with a simple annotation:\n\n",
                                "code":  "// annotations/src/main/kotlin/com/example/AutoBuilder.kt\npackage com.example\n\n/**\n * Generates a builder class for the annotated data class.\n * \n * Usage:\n * ```\n * @AutoBuilder\n * data class User(val id: Long, val name: String)\n * ```\n * \n * Generates:\n * ```\n * class UserBuilder {\n *     var id: Long? = null\n *     var name: String? = null\n *     \n *     fun id(value: Long) = apply { id = value }\n *     fun name(value: String) = apply { name = value }\n *     fun build(): User = User(id = id!!, name = name!!)\n * }\n * ```\n */\n@Target(AnnotationTarget.CLASS)\n@Retention(AnnotationRetention.SOURCE)  // Only needed at compile time\nannotation class AutoBuilder",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Creating the Processor",
                                "content":  "\nThe core processor implementation:\n\n",
                                "code":  "// processor/src/main/kotlin/com/example/AutoBuilderProcessor.kt\npackage com.example\n\nimport com.google.devtools.ksp.processing.*\nimport com.google.devtools.ksp.symbol.*\nimport com.google.devtools.ksp.validate\n\nclass AutoBuilderProcessor(\n    private val codeGenerator: CodeGenerator,\n    private val logger: KSPLogger\n) : SymbolProcessor {\n\n    override fun process(resolver: Resolver): List\u003cKSAnnotated\u003e {\n        // Find all classes annotated with @AutoBuilder\n        val symbols = resolver.getSymbolsWithAnnotation(\n            AutoBuilder::class.qualifiedName!!\n        )\n        \n        // Filter to class declarations that are valid\n        val (valid, invalid) = symbols\n            .filterIsInstance\u003cKSClassDeclaration\u003e()\n            .partition { it.validate() }\n        \n        // Process valid symbols\n        valid.forEach { classDecl -\u003e\n            logger.info(\"Processing: ${classDecl.simpleName.asString()}\")\n            generateBuilder(classDecl)\n        }\n        \n        // Return invalid symbols for reprocessing in next round\n        return invalid\n    }\n    \n    private fun generateBuilder(classDecl: KSClassDeclaration) {\n        val className = classDecl.simpleName.asString()\n        val packageName = classDecl.packageName.asString()\n        val properties = classDecl.getAllProperties().toList()\n        \n        // Validate: must be a data class\n        if (!classDecl.modifiers.contains(Modifier.DATA)) {\n            logger.error(\"@AutoBuilder can only be applied to data classes\", classDecl)\n            return\n        }\n        \n        // Generate builder code using KotlinPoet (next example)\n        generateBuilderWithKotlinPoet(classDecl, packageName, className, properties)\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Generation with KotlinPoet",
                                "content":  "\nKotlinPoet makes generating Kotlin code easy:\n\n",
                                "code":  "// Continue from previous example\nimport com.squareup.kotlinpoet.*\nimport com.squareup.kotlinpoet.ksp.writeTo\nimport com.squareup.kotlinpoet.ksp.toTypeName\n\nprivate fun generateBuilderWithKotlinPoet(\n    classDecl: KSClassDeclaration,\n    packageName: String,\n    className: String,\n    properties: List\u003cKSPropertyDeclaration\u003e\n) {\n    val builderClassName = \"${className}Builder\"\n    \n    // Create the builder class\n    val builderClass = TypeSpec.classBuilder(builderClassName)\n        .apply {\n            // Add nullable properties\n            properties.forEach { prop -\u003e\n                val propName = prop.simpleName.asString()\n                val propType = prop.type.resolve().toTypeName()\n                \n                addProperty(\n                    PropertySpec.builder(propName, propType.copy(nullable = true))\n                        .mutable()\n                        .initializer(\"null\")\n                        .build()\n                )\n            }\n            \n            // Add fluent setter methods\n            properties.forEach { prop -\u003e\n                val propName = prop.simpleName.asString()\n                val propType = prop.type.resolve().toTypeName()\n                \n                addFunction(\n                    FunSpec.builder(propName)\n                        .addParameter(\"value\", propType)\n                        .returns(ClassName(packageName, builderClassName))\n                        .addStatement(\"this.%L = value\", propName)\n                        .addStatement(\"return this\")\n                        .build()\n                )\n            }\n            \n            // Add build() method\n            val buildParams = properties.joinToString(\", \") {\n                \"${it.simpleName.asString()} = ${it.simpleName.asString()}!!\"\n            }\n            \n            addFunction(\n                FunSpec.builder(\"build\")\n                    .returns(ClassName(packageName, className))\n                    .addStatement(\"return %L(%L)\", className, buildParams)\n                    .build()\n            )\n        }\n        .build()\n    \n    // Create the file\n    val fileSpec = FileSpec.builder(packageName, builderClassName)\n        .addType(builderClass)\n        .build()\n    \n    // Write to generated sources\n    fileSpec.writeTo(\n        codeGenerator,\n        Dependencies(true, classDecl.containingFile!!)\n    )\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "The Provider and Registration",
                                "content":  "\nRegister your processor with KSP:\n\n",
                                "code":  "// processor/src/main/kotlin/com/example/AutoBuilderProcessorProvider.kt\npackage com.example\n\nimport com.google.devtools.ksp.processing.*\n\nclass AutoBuilderProcessorProvider : SymbolProcessorProvider {\n    override fun create(environment: SymbolProcessorEnvironment): SymbolProcessor {\n        return AutoBuilderProcessor(\n            codeGenerator = environment.codeGenerator,\n            logger = environment.logger\n        )\n    }\n}\n\n// Register the provider:\n// Create file: processor/src/main/resources/META-INF/services/com.google.devtools.ksp.processing.SymbolProcessorProvider\n// Contents: com.example.AutoBuilderProcessorProvider\n\n// Or use AutoService for automatic registration:\n// processor/build.gradle.kts\ndependencies {\n    implementation(\"com.google.auto.service:auto-service-annotations:1.1.1\")\n    ksp(\"dev.zacsweers.autoservice:auto-service-ksp:1.2.0\")\n}\n\n// Then annotate your provider:\nimport com.google.auto.service.AutoService\n\n@AutoService(SymbolProcessorProvider::class)\nclass AutoBuilderProcessorProvider : SymbolProcessorProvider {\n    // ...\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Using the Generated Code",
                                "content":  "\nThe generated builder in action:\n\n",
                                "code":  "// app/src/main/kotlin/com/example/User.kt\npackage com.example\n\n@AutoBuilder\ndata class User(\n    val id: Long,\n    val name: String,\n    val email: String,\n    val isActive: Boolean\n)\n\n// After compilation, this is generated:\n// build/generated/ksp/main/kotlin/com/example/UserBuilder.kt\n// class UserBuilder {\n//     var id: Long? = null\n//     var name: String? = null\n//     var email: String? = null\n//     var isActive: Boolean? = null\n//     \n//     fun id(value: Long): UserBuilder { this.id = value; return this }\n//     fun name(value: String): UserBuilder { this.name = value; return this }\n//     fun email(value: String): UserBuilder { this.email = value; return this }\n//     fun isActive(value: Boolean): UserBuilder { this.isActive = value; return this }\n//     \n//     fun build(): User = User(\n//         id = id!!,\n//         name = name!!,\n//         email = email!!,\n//         isActive = isActive!!\n//     )\n// }\n\n// Usage:\nfun main() {\n    val user = UserBuilder()\n        .id(1)\n        .name(\"John Doe\")\n        .email(\"john@example.com\")\n        .isActive(true)\n        .build()\n    \n    println(user)  // User(id=1, name=John Doe, email=john@example.com, isActive=true)\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Testing Your Processor",
                                "content":  "\nUse compile-testing to test your processor:\n\n",
                                "code":  "// processor/build.gradle.kts\ndependencies {\n    testImplementation(\"com.github.tschuchortdev:kotlin-compile-testing-ksp:1.6.0\")\n    testImplementation(kotlin(\"test\"))\n}\n\n// processor/src/test/kotlin/AutoBuilderProcessorTest.kt\npackage com.example\n\nimport com.tschuchort.compiletesting.*\nimport org.junit.jupiter.api.Test\nimport kotlin.test.*\n\nclass AutoBuilderProcessorTest {\n    \n    @Test\n    fun `generates builder for data class`() {\n        val source = SourceFile.kotlin(\n            \"User.kt\",\n            \"\"\"\n            package com.example\n            \n            @AutoBuilder\n            data class User(val id: Long, val name: String)\n            \"\"\".trimIndent()\n        )\n        \n        val result = KotlinCompilation().apply {\n            sources = listOf(source)\n            symbolProcessorProviders = listOf(AutoBuilderProcessorProvider())\n            inheritClassPath = true\n        }.compile()\n        \n        assertEquals(KotlinCompilation.ExitCode.OK, result.exitCode)\n        \n        // Check generated file exists\n        val generatedFile = result.kspSourcesDir\n            .walkTopDown()\n            .find { it.name == \"UserBuilder.kt\" }\n        \n        assertNotNull(generatedFile)\n        assertTrue(generatedFile.readText().contains(\"class UserBuilder\"))\n    }\n    \n    @Test\n    fun `fails for non-data class`() {\n        val source = SourceFile.kotlin(\n            \"RegularClass.kt\",\n            \"\"\"\n            package com.example\n            \n            @AutoBuilder\n            class RegularClass(val id: Long)  // Not a data class\n            \"\"\".trimIndent()\n        )\n        \n        val result = KotlinCompilation().apply {\n            sources = listOf(source)\n            symbolProcessorProviders = listOf(AutoBuilderProcessorProvider())\n            inheritClassPath = true\n        }.compile()\n        \n        // Should have error message\n        assertTrue(result.messages.contains(\"data classes\"))\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Lesson Summary",
                                "content":  "\n### KSP Processor Development Summary\n\n1. **Three-module structure**: annotations, processor, and app\n2. **SymbolProcessor** processes annotated symbols\n3. **KotlinPoet** generates clean Kotlin code\n4. **Register via META-INF/services** or AutoService\n5. **Test with kotlin-compile-testing-ksp**\n\n### Key Classes\n\n- `SymbolProcessor` - Your processor logic\n- `SymbolProcessorProvider` - Factory for processor\n- `Resolver` - Access to symbols\n- `CodeGenerator` - Write output files\n- `KSClassDeclaration` - Represents a class\n- `KSPropertyDeclaration` - Represents a property\n\n### Best Practices\n\n- Return unvalidated symbols for reprocessing\n- Log errors with `logger.error()` for clear messages\n- Use `Dependencies` to track file relationships\n- Test with compile-testing library\n\n---\n\n"
                            }
                        ],
    "challenges":  [

                   ],
    "difficulty":  "advanced",
    "title":  "Lesson 10.4: Writing Your First KSP Processor",
    "estimatedMinutes":  75
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current kotlin documentation
- Search the web for the latest kotlin version and verify examples work with it
- Flag any deprecated APIs, syntax, or patterns

### 2. Completeness Check
- Does the lesson cover all concepts needed for a beginner to understand this topic?
- Are there missing explanations between concepts that would confuse a learner?
- Does the lesson have:
  - [ ] A clear analogy or real-world example (ANALOGY section)
  - [ ] Theoretical explanation (THEORY section)
  - [ ] Working code example (EXAMPLE section)
  - [ ] Common mistakes to avoid (WARNING section)
  - [ ] At least one practice challenge

### 3. Freshness Check
- Search for "kotlin Lesson 10.4: Writing Your First KSP Processor 2024 2025" to find latest practices
- Compare lesson content against current best practices
- Identify any outdated patterns or recommendations

### 4. Pedagogical Gap Analysis
- What prerequisite knowledge is assumed but not explained?
- What follow-up questions would a learner likely have?
- What practical applications or use cases are missing?
- Are the challenges appropriately scaffolded for the difficulty level?

## Output Format

Provide your review as structured JSON:

```json
{
  "lessonId": "10.4",
  "reviewDate": "YYYY-MM-DD",
  "overallScore": 1-10,
  "accuracy": {
    "score": 1-10,
    "issues": ["issue 1", "issue 2"],
    "recommendations": ["fix 1", "fix 2"]
  },
  "completeness": {
    "score": 1-10,
    "missingSections": ["section type needed"],
    "gaps": ["gap 1", "gap 2"],
    "recommendations": ["add X", "expand Y"]
  },
  "freshness": {
    "score": 1-10,
    "outdatedItems": ["item 1"],
    "currentVersion": "language version checked",
    "recommendations": ["update X to Y"]
  },
  "pedagogicalGaps": {
    "score": 1-10,
    "missingPrerequisites": ["concept 1"],
    "unansweredQuestions": ["question learner would have"],
    "missingUseCases": ["practical application"],
    "recommendations": ["add section on X"]
  },
  "contentLengthIssues": {
    "shortSections": [
      {"sectionTitle": "title", "currentLength": 42, "recommendation": "expand to explain X"}
    ]
  },
  "suggestedNewContent": [
    {
      "sectionType": "THEORY|EXAMPLE|WARNING|etc",
      "title": "suggested title",
      "contentOutline": "what this section should cover"
    }
  ],
  "priority": "HIGH|MEDIUM|LOW"
}
```

