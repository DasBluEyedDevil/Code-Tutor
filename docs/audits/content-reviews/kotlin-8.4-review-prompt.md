# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Gradle Mastery for Kotlin Developers
- **Lesson:** Lesson 8.4: Custom Tasks and Plugins (ID: 8.4)
- **Difficulty:** intermediate
- **Estimated Time:** 75 minutes

## Current Lesson Content

{
    "id":  "8.4",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 75 minutes\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Topic Introduction",
                                "content":  "\nGradle\u0027s power comes from its extensibility. Custom tasks automate repetitive work, and plugins package reusable build logic.\n\nIn this lesson, you\u0027ll learn:\n- How to create custom Gradle tasks\n- Understanding task dependencies and ordering\n- Creating simple script plugins\n- Understanding task inputs/outputs for caching\n\n---\n\n"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Creating Custom Tasks",
                                "content":  "\nCustom tasks automate project-specific work:\n\n",
                                "code":  "// Custom task in build.gradle.kts\ntasks.register(\"generateBuildInfo\") {\n    group = \"build\"\n    description = \"Generates build information file\"\n\n    val outputFile = layout.buildDirectory.file(\"generated/BuildInfo.kt\")\n    outputs.file(outputFile)\n\n    doLast {\n        outputFile.get().asFile.apply {\n            parentFile.mkdirs()\n            writeText(\"\"\"\n                package com.example\n\n                object BuildInfo {\n                    const val VERSION = \"${project.version}\"\n                    const val BUILD_TIME = \"${java.time.Instant.now()}\"\n                    const val GIT_HASH = \"${getGitHash()}\"\n                }\n            \"\"\".trimIndent())\n        }\n    }\n}\n\nfun getGitHash(): String =\n    providers.exec {\n        commandLine(\"git\", \"rev-parse\", \"--short\", \"HEAD\")\n    }.standardOutput.asText.get().trim()",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Task Dependencies",
                                "content":  "\n### Ordering Tasks\n\nControl when tasks run relative to each other:\n\n```kotlin\n// Task dependency - must run before\ntasks.named(\"compileKotlin\") {\n    dependsOn(\"generateBuildInfo\")\n}\n\n// Must run after (if both are in graph)\ntasks.register(\"cleanup\") {\n    mustRunAfter(\"build\")\n}\n\n// Should run after (weaker ordering)\ntasks.register(\"report\") {\n    shouldRunAfter(\"test\")\n}\n\n// Finalized by (always runs after, even on failure)\ntasks.named(\"test\") {\n    finalizedBy(\"generateReport\")\n}\n```\n\n### Task Graph\n\n```\ncompileKotlin\n    |\n    +-- dependsOn --\u003e generateBuildInfo\n    |\n    v\ntest\n    |\n    +-- finalizedBy --\u003e generateReport\n```\n\n---\n\n"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Typed Tasks",
                                "content":  "\nUse built-in task types for common operations:\n\n",
                                "code":  "// Copy task\ntasks.register\u003cCopy\u003e(\"copyDocs\") {\n    from(\"docs\")\n    into(layout.buildDirectory.dir(\"documentation\"))\n    include(\"**/*.md\")\n    rename { it.replace(\".md\", \".txt\") }\n}\n\n// Delete task\ntasks.register\u003cDelete\u003e(\"cleanGenerated\") {\n    delete(layout.buildDirectory.dir(\"generated\"))\n}\n\n// Zip task\ntasks.register\u003cZip\u003e(\"packageDist\") {\n    from(layout.buildDirectory.dir(\"dist\"))\n    archiveFileName.set(\"app-${project.version}.zip\")\n    destinationDirectory.set(layout.buildDirectory.dir(\"packages\"))\n}\n\n// Exec task\ntasks.register\u003cExec\u003e(\"npmInstall\") {\n    workingDir = file(\"frontend\")\n    commandLine(\"npm\", \"install\")\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Task Inputs and Outputs",
                                "content":  "\n### Incremental Builds\n\nGradle skips tasks when inputs/outputs haven\u0027t changed:\n\n```kotlin\ntasks.register(\"processConfig\") {\n    // Declare inputs\n    inputs.file(\"config.json\")\n    inputs.property(\"version\", project.version)\n    \n    // Declare outputs\n    outputs.file(layout.buildDirectory.file(\"config.processed.json\"))\n    \n    doLast {\n        // Task logic\n    }\n}\n```\n\n### Why It Matters\n\n- First run: Task executes\n- Second run (no changes): `UP-TO-DATE` (skipped)\n- Change input: Task executes again\n\nThis can save minutes on large builds!\n\n---\n\n"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Creating a Script Plugin",
                                "content":  "\nExtract reusable build logic into script plugins:\n\n",
                                "code":  "// gradle/quality.gradle.kts\n// A reusable script plugin for code quality\n\nval detektTask = tasks.register(\"detektCheck\") {\n    group = \"verification\"\n    description = \"Runs Detekt static analysis\"\n    \n    doLast {\n        println(\"Running Detekt analysis...\")\n        // Detekt logic here\n    }\n}\n\nval ktlintTask = tasks.register(\"ktlintCheck\") {\n    group = \"verification\"\n    description = \"Runs ktlint style check\"\n    \n    doLast {\n        println(\"Running ktlint check...\")\n        // ktlint logic here\n    }\n}\n\ntasks.named(\"check\") {\n    dependsOn(detektTask, ktlintTask)\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Applying Script Plugins",
                                "content":  "\nApply the plugin in your build script:\n\n",
                                "code":  "// build.gradle.kts\nplugins {\n    kotlin(\"jvm\") version \"2.0.21\"\n}\n\n// Apply the script plugin\napply(from = \"gradle/quality.gradle.kts\")\n\n// Now quality tasks are available:\n// ./gradlew detektCheck\n// ./gradlew ktlintCheck\n// ./gradlew check (includes both)",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Practical Task: Generate API Client",
                                "content":  "\nA real-world example of code generation:\n\n",
                                "code":  "tasks.register(\"generateApiClient\") {\n    group = \"codegen\"\n    description = \"Generates API client from OpenAPI spec\"\n    \n    val specFile = file(\"api/openapi.yaml\")\n    val outputDir = layout.buildDirectory.dir(\"generated/api\")\n    \n    inputs.file(specFile)\n    outputs.dir(outputDir)\n    \n    doLast {\n        val spec = specFile.readText()\n        val output = outputDir.get().asFile\n        output.mkdirs()\n        \n        // Parse spec and generate client code\n        println(\"Generating API client from ${specFile.name}\")\n        \n        // Add generated sources to compilation\n        val generatedFile = File(output, \"ApiClient.kt\")\n        generatedFile.writeText(\"\"\"\n            package com.example.api\n            \n            class ApiClient {\n                // Generated from OpenAPI spec\n            }\n        \"\"\".trimIndent())\n    }\n}\n\n// Add generated sources to source sets\nkotlin {\n    sourceSets {\n        main {\n            kotlin.srcDir(layout.buildDirectory.dir(\"generated/api\"))\n        }\n    }\n}\n\ntasks.named(\"compileKotlin\") {\n    dependsOn(\"generateApiClient\")\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "WARNING",
                                "title":  "Common Task Mistakes",
                                "content":  "\n### Doing Work at Configuration Time\n\n```kotlin\n// WRONG - runs during configuration\ntasks.register(\"bad\") {\n    val content = file(\"data.txt\").readText()  // Runs immediately!\n    doLast {\n        println(content)\n    }\n}\n\n// RIGHT - runs during execution\ntasks.register(\"good\") {\n    doLast {\n        val content = file(\"data.txt\").readText()  // Runs when task executes\n        println(content)\n    }\n}\n```\n\n### Not Declaring Inputs/Outputs\n\nWithout proper declarations, tasks always run:\n\n```kotlin\n// WRONG - no caching\ntasks.register(\"process\") {\n    doLast { /* work */ }\n}\n\n// RIGHT - proper caching\ntasks.register(\"process\") {\n    inputs.file(\"input.txt\")\n    outputs.file(layout.buildDirectory.file(\"output.txt\"))\n    doLast { /* work */ }\n}\n```\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise: Create Build Info Task",
                                "content":  "\n**Goal**: Create a task that generates a `BuildInfo.kt` file.\n\n**Requirements**:\n1. Generate file with version, build time, and git hash\n2. Output to `build/generated/BuildInfo.kt`\n3. Make compileKotlin depend on it\n4. Declare proper inputs/outputs for caching\n5. Only regenerate when version changes\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Key Takeaways",
                                "content":  "\n- Tasks are the building blocks of Gradle builds\n- Use `dependsOn`, `mustRunAfter`, and `finalizedBy` for ordering\n- Declare inputs/outputs for incremental builds\n- Script plugins share logic across modules\n- Always do work in `doLast { }`, not at configuration time\n\n---\n\n"
                            }
                        ],
    "challenges":  [

                   ],
    "difficulty":  "intermediate",
    "title":  "Lesson 8.4: Custom Tasks and Plugins",
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
- Search for "kotlin Lesson 8.4: Custom Tasks and Plugins 2024 2025" to find latest practices
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
  "lessonId": "8.4",
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

