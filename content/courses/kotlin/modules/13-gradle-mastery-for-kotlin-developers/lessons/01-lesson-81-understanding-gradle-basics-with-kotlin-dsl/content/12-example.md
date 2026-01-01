---
type: "EXAMPLE"
title: "Solution: Project Setup"
---




```bash
# Create project
mkdir my-project && cd my-project

# Initialize Gradle
gradle wrapper

# Create settings.gradle.kts
echo 'rootProject.name = "my-project"' > settings.gradle.kts

# Create build.gradle.kts (see content below)

# Create source directory
mkdir -p src/main/kotlin/com/example

# Create Main.kt
cat > src/main/kotlin/com/example/Main.kt << 'EOF'
package com.example

fun main() {
    println("Hello from Gradle!")
}
EOF

# Run
./gradlew run
```
