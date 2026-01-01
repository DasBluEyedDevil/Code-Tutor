---
type: "KEY_POINT"
title: "Kotlin DSL vs Groovy"
---

TWO WAYS TO WRITE GRADLE FILES:

KOTLIN DSL (.gradle.kts):
plugins {
    kotlin("jvm") version "2.1.0"
}
dependencies {
    implementation("com.google.guava:guava:32.1.2-jre")
}

GROOVY DSL (.gradle):
plugins {
    id 'org.jetbrains.kotlin.jvm' version '2.1.0'
}
dependencies {
    implementation 'com.google.guava:guava:32.1.2-jre'
}

WHY KOTLIN DSL IS PREFERRED:
✓ Type safety (IDE catches errors)
✓ Better autocomplete in IntelliJ
✓ Easier refactoring
✓ More readable for Kotlin developers

Most new projects use Kotlin DSL (.gradle.kts)