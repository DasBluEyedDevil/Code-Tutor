---
type: "THEORY"
title: "Publishing KMP Libraries"
---


### Why Publish KMP Libraries?

- Share code across your organization's projects
- Contribute to the open-source KMP ecosystem
- Create SDKs that work on all platforms

### Setting Up for Publishing

```kotlin
// build.gradle.kts
plugins {
    kotlin("multiplatform")
    id("maven-publish")
    id("signing")  // For Maven Central
}

group = "com.yourcompany"
version = "1.0.0"

kotlin {
    // Define all targets
    jvm()
    androidTarget {
        publishLibraryVariants("release")
    }
    iosX64()
    iosArm64()
    iosSimulatorArm64()
    js(IR) { browser(); nodejs() }
    linuxX64()
    macosX64()
    macosArm64()

    sourceSets {
        val commonMain by getting {
            dependencies {
                implementation("org.jetbrains.kotlinx:kotlinx-coroutines-core:1.10.2")
            }
        }
    }
}

publishing {
    publications {
        // KMP plugin auto-generates publications
    }
    
    repositories {
        maven {
            name = "GitHubPackages"
            url = uri("https://maven.pkg.github.com/yourorg/yourrepo")
            credentials {
                username = System.getenv("GITHUB_ACTOR")
                password = System.getenv("GITHUB_TOKEN")
            }
        }
    }
}
```

### Publishing to Maven Central

```kotlin
signing {
    sign(publishing.publications)
}

publishing {
    publications.withType<MavenPublication> {
        pom {
            name.set("My KMP Library")
            description.set("A cross-platform library")
            url.set("https://github.com/yourorg/yourrepo")
            
            licenses {
                license {
                    name.set("Apache-2.0")
                    url.set("https://opensource.org/licenses/Apache-2.0")
                }
            }
            
            developers {
                developer {
                    id.set("yourid")
                    name.set("Your Name")
                }
            }
            
            scm {
                connection.set("scm:git:git://github.com/yourorg/yourrepo.git")
                url.set("https://github.com/yourorg/yourrepo")
            }
        }
    }
}
```

### Publishing Commands

```bash
# Publish to local Maven (~/.m2)
./gradlew publishToMavenLocal

# Publish to GitHub Packages
./gradlew publish

# Publish to Maven Central (with signing)
./gradlew publishAllPublicationsToSonatypeRepository
```

---

