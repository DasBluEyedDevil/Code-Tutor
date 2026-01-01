# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** The K2 Era - Modern Kotlin Tooling
- **Lesson:** Lesson 10.3: KSP - Replacing kapt with Speed (ID: 10.3)
- **Difficulty:** advanced
- **Estimated Time:** 60 minutes

## Current Lesson Content

{
    "id":  "10.3",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 60 minutes\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Topic Introduction",
                                "content":  "\nKSP (Kotlin Symbol Processing) is a modern alternative to kapt that\u0027s significantly faster and designed specifically for Kotlin. If you\u0027re using kapt for annotation processing, migrating to KSP can cut your build times substantially.\n\nIn this lesson, you\u0027ll learn:\n- Why KSP is faster than kapt\n- How to migrate common libraries from kapt to KSP\n- KSP configuration and optimization\n- Troubleshooting KSP issues\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Why KSP Over kapt?",
                                "content":  "\n### The Problem with kapt\n\nkapt (Kotlin Annotation Processing Tool) works by:\n1. Generating Java stubs from Kotlin code\n2. Running Java annotation processors on those stubs\n3. Processing the generated code\n\nThis has significant drawbacks:\n\n- **Slow**: Stub generation is expensive\n- **Memory intensive**: Maintains two representations\n- **Loses Kotlin information**: Processors see Java, not Kotlin\n- **Limited incremental processing**: Often requires full rebuild\n\n### KSP Advantages\n\n| Feature | kapt | KSP |\n|---------|------|-----|\n| Speed | Baseline | **2x faster** |\n| Memory | High | **Lower** |\n| Kotlin-aware | No | **Yes** |\n| Incremental | Limited | **Full** |\n| K2 Compatible | Works but slow | **Native** |\n\n---\n\n"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Migrating Room to KSP",
                                "content":  "\nRoom is one of the most common kapt users. Here\u0027s how to migrate:\n\n",
                                "code":  "// Before: build.gradle.kts with kapt\nplugins {\n    id(\"org.jetbrains.kotlin.kapt\")\n}\n\ndependencies {\n    implementation(\"androidx.room:room-runtime:2.6.1\")\n    implementation(\"androidx.room:room-ktx:2.6.1\")\n    kapt(\"androidx.room:room-compiler:2.6.1\")\n}\n\n// After: build.gradle.kts with KSP\nplugins {\n    id(\"com.google.devtools.ksp\") version \"2.0.21-1.0.28\"\n}\n\ndependencies {\n    implementation(\"androidx.room:room-runtime:2.6.1\")\n    implementation(\"androidx.room:room-ktx:2.6.1\")\n    ksp(\"androidx.room:room-compiler:2.6.1\")  // Changed from kapt\n}\n\n// KSP-specific configuration for Room\nksp {\n    arg(\"room.schemaLocation\", \"$projectDir/schemas\")\n    arg(\"room.incremental\", \"true\")\n    arg(\"room.generateKotlin\", \"true\")  // Generate Kotlin instead of Java\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Migrating Moshi to KSP",
                                "content":  "\nMoshi code generation also supports KSP:\n\n",
                                "code":  "// Before: kapt\nplugins {\n    id(\"org.jetbrains.kotlin.kapt\")\n}\n\ndependencies {\n    implementation(\"com.squareup.moshi:moshi:1.15.1\")\n    kapt(\"com.squareup.moshi:moshi-kotlin-codegen:1.15.1\")\n}\n\n// After: KSP\nplugins {\n    id(\"com.google.devtools.ksp\") version \"2.0.21-1.0.28\"\n}\n\ndependencies {\n    implementation(\"com.squareup.moshi:moshi:1.15.1\")\n    ksp(\"com.squareup.moshi:moshi-kotlin-codegen:1.15.1\")\n}\n\n// Usage remains the same:\n@JsonClass(generateAdapter = true)\ndata class User(\n    @Json(name = \"user_id\") val id: Long,\n    val name: String,\n    val email: String\n)\n\n// KSP generates UserJsonAdapter.kt (not .java!)",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Migrating Dagger/Hilt to KSP",
                                "content":  "\nDagger and Hilt now support KSP:\n\n",
                                "code":  "// Before: kapt for Hilt\nplugins {\n    id(\"org.jetbrains.kotlin.kapt\")\n    id(\"com.google.dagger.hilt.android\")\n}\n\ndependencies {\n    implementation(\"com.google.dagger:hilt-android:2.51.1\")\n    kapt(\"com.google.dagger:hilt-android-compiler:2.51.1\")\n}\n\n// After: KSP for Hilt\nplugins {\n    id(\"com.google.devtools.ksp\") version \"2.0.21-1.0.28\"\n    id(\"com.google.dagger.hilt.android\")\n}\n\ndependencies {\n    implementation(\"com.google.dagger:hilt-android:2.51.1\")\n    ksp(\"com.google.dagger:hilt-android-compiler:2.51.1\")\n}\n\n// For plain Dagger:\ndependencies {\n    implementation(\"com.google.dagger:dagger:2.51.1\")\n    ksp(\"com.google.dagger:dagger-compiler:2.51.1\")\n}\n\n// Note: Remove kapt plugin if no longer needed\n// plugins {\n//     id(\"org.jetbrains.kotlin.kapt\")  // Remove this\n// }",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Migrating Koin Annotations to KSP",
                                "content":  "\nKoin annotations use KSP for dependency injection:\n\n",
                                "code":  "// build.gradle.kts with KSP for Koin\nplugins {\n    id(\"com.google.devtools.ksp\") version \"2.0.21-1.0.28\"\n}\n\ndependencies {\n    implementation(\"io.insert-koin:koin-core:4.0.0\")\n    implementation(\"io.insert-koin:koin-annotations:1.4.0\")\n    ksp(\"io.insert-koin:koin-ksp-compiler:1.4.0\")\n}\n\n// Enable generated code\nkotlin {\n    sourceSets.main {\n        kotlin.srcDir(\"build/generated/ksp/main/kotlin\")\n    }\n}\n\n// Usage with annotations:\n@Module\n@ComponentScan(\"com.example.app\")\nclass AppModule\n\n@Single\nclass UserRepository(\n    private val database: Database\n) {\n    fun getUsers(): List\u003cUser\u003e = database.users()\n}\n\n@Factory\nclass GetUsersUseCase(\n    private val repository: UserRepository\n) {\n    operator fun invoke(): List\u003cUser\u003e = repository.getUsers()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "KSP Configuration Options",
                                "content":  "\nOptimize KSP for your project:\n\n",
                                "code":  "// build.gradle.kts\n\nksp {\n    // Pass arguments to processors\n    arg(\"option.name\", \"value\")\n    \n    // Room-specific arguments\n    arg(\"room.schemaLocation\", \"$projectDir/schemas\")\n    arg(\"room.incremental\", \"true\")\n    arg(\"room.generateKotlin\", \"true\")\n    \n    // Moshi arguments\n    arg(\"moshi.generated\", \"javax.annotation.processing.Generated\")\n}\n\n// For multiplatform projects:\nkotlin {\n    sourceSets {\n        commonMain {\n            kotlin.srcDir(\"build/generated/ksp/metadata/commonMain/kotlin\")\n        }\n    }\n}\n\n// Add generated sources to source sets\nandroid {\n    applicationVariants.all {\n        val variantName = name\n        sourceSets {\n            getByName(\"main\") {\n                java.srcDir(\"build/generated/ksp/$variantName/kotlin\")\n            }\n        }\n    }\n}\n\n// Performance optimization in gradle.properties\n// ksp.incremental=true              # Enable incremental processing\n// ksp.incremental.log=true          # Log incremental decisions",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Libraries Supporting KSP",
                                "content":  "\n### Major Libraries with KSP Support\n\n**Android/JVM**\n- Room 2.4+\n- Moshi 1.13+\n- Dagger/Hilt 2.48+\n- Koin 3.5+ (annotations)\n- Glide 4.14+\n- Lyricist (i18n)\n- Ktorfit\n\n**Multiplatform**\n- kotlinx.serialization (built-in, no KSP needed)\n- SQLDelight\n- Apollo GraphQL\n- Koin Annotations\n\n**Still Require kapt**\n- Some older libraries\n- Custom annotation processors not yet migrated\n- MapStruct (migration in progress)\n\n---\n\n"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Mixing kapt and KSP",
                                "content":  "\nSome projects need both during migration:\n\n",
                                "code":  "// build.gradle.kts - Using both kapt and KSP\nplugins {\n    kotlin(\"jvm\") version \"2.0.21\"\n    id(\"org.jetbrains.kotlin.kapt\")  // Keep for legacy processors\n    id(\"com.google.devtools.ksp\") version \"2.0.21-1.0.28\"\n}\n\ndependencies {\n    // KSP-migrated libraries\n    implementation(\"androidx.room:room-runtime:2.6.1\")\n    ksp(\"androidx.room:room-compiler:2.6.1\")\n    \n    implementation(\"com.squareup.moshi:moshi:1.15.1\")\n    ksp(\"com.squareup.moshi:moshi-kotlin-codegen:1.15.1\")\n    \n    // Legacy library still on kapt\n    implementation(\"some.legacy:library:1.0.0\")\n    kapt(\"some.legacy:processor:1.0.0\")\n}\n\n// Note: Having both adds overhead\n// Plan to remove kapt entirely when possible\n\n// Order matters! KSP runs before kapt by default\n// If you need kapt to run first:\ntasks.withType\u003corg.jetbrains.kotlin.gradle.tasks.KotlinCompile\u003e {\n    dependsOn(\"kaptKotlin\")\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Lesson Summary",
                                "content":  "\n### KSP Migration Summary\n\n1. **KSP is 2x faster than kapt** - worth the migration effort\n2. **Most major libraries support KSP** - Room, Moshi, Dagger, Koin\n3. **Migration is usually simple** - change `kapt` to `ksp` in dependencies\n4. **Configure KSP arguments** using the `ksp { }` block\n5. **You can mix kapt and KSP** during gradual migration\n6. **Remove kapt plugin** when fully migrated for best performance\n\n### Migration Command\n\n```bash\n# Quick find-and-replace in build files:\n# kapt(...) -\u003e ksp(...)\n# id(\"org.jetbrains.kotlin.kapt\") -\u003e remove if not needed\n```\n\n### Next Steps\n\nIn the next lesson, you\u0027ll learn how to write your own KSP processor to generate code.\n\n---\n\n"
                            }
                        ],
    "challenges":  [

                   ],
    "difficulty":  "advanced",
    "title":  "Lesson 10.3: KSP - Replacing kapt with Speed",
    "estimatedMinutes":  60
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
- Search for "kotlin Lesson 10.3: KSP - Replacing kapt with Speed 2024 2025" to find latest practices
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
  "lessonId": "10.3",
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

