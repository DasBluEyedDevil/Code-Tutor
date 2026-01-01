---
type: "KEY_POINT"
title: "Converting Maven to Gradle"
---

AUTOMATIC CONVERSION:

# In your Maven project directory:
gradle init --type pom

This reads pom.xml and creates:
- build.gradle.kts (or build.gradle)
- settings.gradle.kts
- gradlew (wrapper script)

MANUAL MAPPING:

Maven pom.xml → Gradle build.gradle.kts:

<groupId> → group = "com.example"
<artifactId> → (in settings.gradle.kts: rootProject.name)
<version> → version = "1.0.0"
<dependencies> → dependencies { }
<build><plugins> → plugins { }

DEPENDENCY SCOPES:
Maven compile → Gradle implementation
Maven test → Gradle testImplementation
Maven provided → Gradle compileOnly
Maven runtime → Gradle runtimeOnly