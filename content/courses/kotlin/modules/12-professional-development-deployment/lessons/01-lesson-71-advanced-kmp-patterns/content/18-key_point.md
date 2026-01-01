---
type: "KEY_POINT"
title: "Compose Multiplatform - Share UI Across Platforms"
---


**Compose Multiplatform** takes code sharing to the next level - you can share not just business logic, but also **UI code** across Android, iOS, Desktop, and Web!

### What is Compose Multiplatform?

Developed by JetBrains, Compose Multiplatform brings Jetpack Compose's declarative UI paradigm to all platforms:

- **Android**: Uses Jetpack Compose directly
- **iOS**: Compiles Compose to native iOS views
- **Desktop**: Native Windows, macOS, Linux apps
- **Web**: Kotlin/JS with Compose HTML

### Code Sharing Comparison

| What You Share | KMP Only | KMP + Compose Multiplatform |
|----------------|----------|-----------------------------|
| Business Logic | ✅ Yes | ✅ Yes |
| Data Models | ✅ Yes | ✅ Yes |
| API Clients | ✅ Yes | ✅ Yes |
| **UI Components** | ❌ No | ✅ Yes! |
| **Screens** | ❌ No | ✅ Yes! |

### Compose Multiplatform Example

```kotlin
// shared/src/commonMain/kotlin/ui/ProductCard.kt
// This UI code works on Android, iOS, Desktop, and Web!

@Composable
fun ProductCard(product: Product, onAddToCart: () -> Unit) {
    Card(
        modifier = Modifier
            .fillMaxWidth()
            .padding(8.dp)
    ) {
        Column(modifier = Modifier.padding(16.dp)) {
            Text(
                text = product.name,
                style = MaterialTheme.typography.headlineSmall
            )
            
            Text(
                text = "$${product.price}",
                style = MaterialTheme.typography.bodyLarge,
                color = MaterialTheme.colorScheme.primary
            )
            
            Spacer(modifier = Modifier.height(8.dp))
            
            Button(onClick = onAddToCart) {
                Text("Add to Cart")
            }
        }
    }
}

// This composable runs on:
// - Android phone
// - iPhone/iPad
// - Windows/macOS/Linux desktop
// - Web browser
```

### Project Structure with Compose Multiplatform

```
project/
├── shared/
│   └── src/
│       ├── commonMain/
│       │   └── kotlin/
│       │       ├── ui/           # Shared UI components!
│       │       │   ├── screens/
│       │       │   ├── components/
│       │       │   └── theme/
│       │       ├── viewmodel/    # Shared ViewModels
│       │       └── data/         # Shared data layer
│       └── iosMain/
│           └── kotlin/           # iOS-specific if needed
├── androidApp/                    # Android entry point
├── iosApp/                        # iOS entry point (Swift/SwiftUI)
├── desktopApp/                    # Desktop entry point
└── webApp/                        # Web entry point
```

### Platform-Specific UI When Needed

```kotlin
// expect/actual for platform-specific UI behavior
expect fun getPlatformName(): String

@Composable
fun WelcomeScreen() {
    Text("Running on: ${getPlatformName()}")
}

// androidMain
actual fun getPlatformName() = "Android"

// iosMain  
actual fun getPlatformName() = "iOS"

// desktopMain
actual fun getPlatformName() = "Desktop"
```

### Real-World Usage

**Companies using Compose Multiplatform:**
- **JetBrains** - Toolbox App, Space
- **McDonald's** - Mobile apps
- **Philips** - Medical device UIs

**When to Use Compose Multiplatform:**
✅ New projects targeting multiple platforms
✅ Internal/enterprise apps where consistent UX matters
✅ Rapid prototyping across platforms
✅ Teams with strong Kotlin expertise

**When to Use Platform-Native UI:**
✅ Apps requiring deep platform integration
✅ Games with custom rendering
✅ Apps where "native feel" is critical

### Getting Started

```kotlin
// build.gradle.kts
plugins {
    kotlin("multiplatform")
    id("org.jetbrains.compose") version "1.6.0"
}

kotlin {
    androidTarget()
    iosX64()
    iosArm64()
    jvm("desktop")
    
    sourceSets {
        val commonMain by getting {
            dependencies {
                implementation(compose.runtime)
                implementation(compose.foundation)
                implementation(compose.material3)
                implementation(compose.ui)
            }
        }
    }
}
```

**Learn More**: https://www.jetbrains.com/lp/compose-multiplatform/

---

