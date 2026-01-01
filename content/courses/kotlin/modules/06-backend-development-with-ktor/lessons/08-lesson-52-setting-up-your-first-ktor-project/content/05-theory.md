---
type: "THEORY"
title: "💻 Creating Your First Ktor Project"
---


### Method 1: Using the Ktor Project Generator (Recommended for Beginners)

1. **Visit the Generator**
   - Open your browser and go to: https://start.ktor.io/

2. **Configure Your Project**
   ```
   Project Name: my-first-api
   Build System: Gradle Kotlin
   Website: example.com
   Artifact: com.example.myfirstapi
   Ktor Version: 3.2.0 (or latest)
   Engine: CIO
   Configuration: Code (not YAML/HOCON for now)
   ```

3. **Add Plugins**
   - **Routing**: For defining endpoints (essential!)
   - **Content Negotiation**: For JSON support (essential!)
   - **kotlinx.serialization**: For converting objects to/from JSON

4. **Generate and Download**
   - Click "Generate Project"
   - Download the ZIP file
   - Extract it to your projects folder

### Method 2: Manual Setup with Gradle (For Understanding)

If you want to understand every piece, let's build it manually:

**Step 1: Create a new directory**

**Step 2: Create the Gradle build file**

Create `build.gradle.kts`:


**Step 3: Create Gradle wrapper files**

Create `gradle.properties`:

Create `settings.gradle.kts`:

---



```kotlin
rootProject.name = "my-first-api"
```
