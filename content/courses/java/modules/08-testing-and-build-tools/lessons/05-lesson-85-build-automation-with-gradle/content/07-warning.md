---
type: "WARNING"
title: "Common Gradle Pitfalls"
---

AVOID THESE MISTAKES:

1. NOT USING THE WRAPPER
   Always use ./gradlew instead of gradle to ensure consistent versions.

2. MIXING GROOVY AND KOTLIN DSL
   Pick one and stick with it. Kotlin DSL (.gradle.kts) is recommended for new projects.

3. APPLYING PLUGINS THE OLD WAY
   Use plugins { } block, not apply plugin: 'java' (deprecated).

4. NOT CONFIGURING TEST PLATFORM
   For JUnit 5, add tasks.test { useJUnitPlatform() }.

5. COMMITTING .gradle FOLDER
   Add .gradle/ to .gitignore - it contains local cache files.