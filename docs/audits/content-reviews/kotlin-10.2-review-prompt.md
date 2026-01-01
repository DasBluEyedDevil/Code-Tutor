# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** The K2 Era - Modern Kotlin Tooling
- **Lesson:** Lesson 10.2: Migrating Projects to K2 (ID: 10.2)
- **Difficulty:** advanced
- **Estimated Time:** 50 minutes

## Current Lesson Content

{
    "id":  "10.2",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 50 minutes\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Topic Introduction",
                                "content":  "\nMigrating to K2 is generally straightforward, but some projects may encounter breaking changes. This lesson covers the migration process systematically.\n\nIn this lesson, you\u0027ll learn:\n- How to assess your project\u0027s K2 readiness\n- Common migration issues and how to fix them\n- A step-by-step migration checklist\n- How to handle breaking changes\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Migration Checklist",
                                "content":  "\n### Pre-Migration Checklist\n\nBefore enabling K2, verify these items:\n\n**1. Kotlin Version**\n- Update to Kotlin 2.0.21 or later\n- Update Kotlin Gradle Plugin\n\n**2. Dependencies**\n- Check all Kotlin libraries support 2.0\n- Update kotlinx libraries (coroutines, serialization, etc.)\n- Verify annotation processors support K2/KSP\n\n**3. Build Plugins**\n- Update Compose compiler plugin (now bundled)\n- Migrate kapt to KSP where possible\n- Update any custom compiler plugins\n\n**4. IDE**\n- Update to IntelliJ IDEA 2024.1+ or Android Studio Koala+\n- Enable K2 IDE mode for testing\n\n---\n\n"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Enabling K2 Progressively",
                                "content":  "\nStart with language version, then move to API version:\n\n",
                                "code":  "// gradle/libs.versions.toml\n[versions]\nkotlin = \"2.0.21\"\nkotlinx-coroutines = \"1.9.0\"\nkotlinx-serialization = \"1.7.3\"\n\n// Step 1: Update Kotlin version in build.gradle.kts\nplugins {\n    kotlin(\"jvm\") version libs.versions.kotlin\n}\n\n// Step 2: Enable K2 with language version first\nkotlin {\n    compilerOptions {\n        // Start with just language version\n        languageVersion.set(org.jetbrains.kotlin.gradle.dsl.KotlinVersion.KOTLIN_2_0)\n        \n        // Build and test your project\n        // Fix any issues that arise\n    }\n}\n\n// Step 3: After successful testing, also set API version\nkotlin {\n    compilerOptions {\n        languageVersion.set(org.jetbrains.kotlin.gradle.dsl.KotlinVersion.KOTLIN_2_0)\n        apiVersion.set(org.jetbrains.kotlin.gradle.dsl.KotlinVersion.KOTLIN_2_0)\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Stricter Null Checking",
                                "content":  "\nK2 is stricter about null safety:\n\n",
                                "code":  "// Issue: K2 catches guaranteed null dereferences\n\n// Before (K1 allowed this):\nval map: Map\u003cString, String\u003e = mapOf()\nval value: String = map[\"key\"]!!  // K2 warns: always null\n\n// After (fix the logic):\nval value: String = map[\"key\"] ?: error(\"Key not found\")\n// Or:\nval value: String = map.getValue(\"key\")  // Throws if missing\n\n// Issue: Platform type handling is stricter\n// Java method: String getName() - could return null\n\n// Before (K1 was lenient):\nval name: String = javaObject.name  // Might crash at runtime\n\n// After (K2 warns):\nval name: String = javaObject.name ?: \"\"  // Handle null explicitly\n// Or:\nval name: String = requireNotNull(javaObject.name) { \"Name cannot be null\" }\n\n// Issue: Nullable receivers in extensions\n// Before:\nfun String.process() = this.uppercase()\nval result = nullableString.process()  // K1 allowed\n\n// After (K2 requires safe call):\nval result = nullableString?.process()\n// Or define extension on nullable:\nfun String?.processSafe() = this?.uppercase() ?: \"\"",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Property Inference Changes",
                                "content":  "\nSome type inference may differ:\n\n",
                                "code":  "// Issue: Complex expression inference\n\n// K1 might infer differently than K2 in edge cases\nval items = listOf(1, 2, 3)\n\n// Before (K1):\nval processed = items.map { \n    if (it \u003e 0) it.toString() else null \n}  // K1 might infer List\u003cString?\u003e\n\n// After (K2) - be explicit when needed:\nval processed: List\u003cString?\u003e = items.map { \n    if (it \u003e 0) it.toString() else null \n}\n\n// Issue: Lambda return type inference\nfun process(block: () -\u003e Any): Any = block()\n\n// Before (K1 - worked):\nval result = process {\n    if (condition) \"string\"\n    else 42\n}  // K1 inferred Any\n\n// After (K2) - explicit type may be needed:\nval result: Any = process {\n    if (condition) \"string\" else 42\n}\n\n// Issue: Generic type inference\nfun \u003cT\u003e identity(value: T): T = value\n\n// K2 may require explicit types in complex chains:\nval result = identity(listOf(1, 2, 3))\n    .map { it * 2 }\n    .filter { it \u003e 2 }  // Usually works\n\n// If issues occur, add explicit type:\nval result: List\u003cInt\u003e = identity\u003cList\u003cInt\u003e\u003e(listOf(1, 2, 3))\n    .map { it * 2 }\n    .filter { it \u003e 2 }",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Library Compatibility Check",
                                "content":  "\nVerify your dependencies support Kotlin 2.0:\n\n",
                                "code":  "// Check dependency versions\n// Run: ./gradlew dependencies --configuration compileClasspath\n\n// Common libraries with K2 support:\n\n// gradle/libs.versions.toml\n[versions]\nkotlin = \"2.0.21\"\nkotlinx-coroutines = \"1.9.0\"       # Full K2 support\nkotlinx-serialization = \"1.7.3\"    # Full K2 support\nktor = \"3.0.2\"                     # Full K2 support\nkoin = \"4.0.0\"                     # Full K2 support (use ksp)\narrow = \"1.2.4\"                    # Full K2 support\ncompose-multiplatform = \"1.7.1\"    # Full K2 support\n\n// Libraries to check/update:\n// - Room: 2.6.0+ supports KSP\n// - Moshi: 1.15.0+ supports KSP\n// - Dagger/Hilt: Use KSP mode\n\n// build.gradle.kts - Check for kapt usage\ndependencies {\n    // Replace kapt with ksp where possible:\n    // kapt(\"com.google.dagger:hilt-compiler:2.51.1\")  // Old\n    ksp(\"com.google.dagger:hilt-compiler:2.51.1\")     // New\n    \n    // Some libraries still require kapt:\n    // kapt(\"some.legacy:processor:1.0\")  // Check for updates\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Common Migration Issues",
                                "content":  "\n### Known Breaking Changes\n\n**1. Overload Resolution**\nK2 may resolve overloads differently in rare cases. If you see unexpected method calls, add explicit types.\n\n**2. Visibility Checks**\nK2 is stricter about visibility. Internal members from dependencies may become inaccessible.\n\n**3. Type Approximation**\nAnonymous types are approximated differently. If types change unexpectedly, add explicit type annotations.\n\n**4. SAM Conversions**\nSome edge cases in SAM conversion work differently. Use explicit lambda types if issues occur.\n\n**5. Annotation Processing**\nkapt works but is slower. Migrate to KSP for better performance.\n\n---\n\n"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Troubleshooting Guide",
                                "content":  "\nCommon issues and their solutions:\n\n",
                                "code":  "// Issue: \"Cannot access class\" errors\n// Solution: The class visibility changed, use public API instead\n\n// Issue: \"Type mismatch\" after K2 upgrade\n// Solution: Add explicit type annotations\nval data: Map\u003cString, Any\u003e = response.body()  // Be explicit\n\n// Issue: Smart cast stopped working\n// Cause: K2 is stricter about mutability\nvar value: Any? = getValue()\nif (value != null) {\n    // K2 may not smart cast if \u0027value\u0027 could change\n    // Solution: Use val or local copy\n    val localValue = value\n    if (localValue != null) {\n        println(localValue.toString())  // Works\n    }\n}\n\n// Issue: Overload resolution changed\n// K2 may pick a different overload\nfun process(value: Int) = println(\"Int: $value\")\nfun process(value: Number) = println(\"Number: $value\")\n\n// Solution: Be explicit about which overload\nprocess(42)           // May differ\nprocess(42 as Int)    // Explicitly Int\nprocess(42 as Number) // Explicitly Number\n\n// Issue: Build fails with kapt\n// Solution: Migrate to KSP or update kapt version\nplugins {\n    // id(\"org.jetbrains.kotlin.kapt\")  // Remove if possible\n    id(\"com.google.devtools.ksp\") version \"2.0.21-1.0.28\"\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Lesson Summary",
                                "content":  "\n### Migration Summary\n\n1. **Update Kotlin to 2.0.21+** in your version catalog\n2. **Enable K2 progressively** - language version first, then API version\n3. **Check dependencies** - ensure all libraries support Kotlin 2.0\n4. **Fix stricter null checks** - K2 catches more null issues\n5. **Add explicit types** where inference changes\n6. **Migrate kapt to KSP** for better performance\n\n### Testing Your Migration\n\n```bash\n# Build and run tests\n./gradlew build\n\n# Check for warnings\n./gradlew compileKotlin --warning-mode all\n\n# Run your test suite\n./gradlew test\n```\n\n### Next Steps\n\nIn the next lesson, you\u0027ll learn about KSP and how to migrate from kapt for faster builds.\n\n---\n\n"
                            }
                        ],
    "challenges":  [

                   ],
    "difficulty":  "advanced",
    "title":  "Lesson 10.2: Migrating Projects to K2",
    "estimatedMinutes":  50
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
- Search for "kotlin Lesson 10.2: Migrating Projects to K2 2024 2025" to find latest practices
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
  "lessonId": "10.2",
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

