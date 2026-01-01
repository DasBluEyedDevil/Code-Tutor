// shared/build.gradle.kts
plugins {
    kotlin("multiplatform")
    id("com.android.library")
    // TODO: Add SQLDelight plugin
}

kotlin {
    androidTarget()
    iosX64()
    iosArm64()
    iosSimulatorArm64()
    jvm("desktop")
    
    sourceSets {
        commonMain.dependencies {
            // TODO: Add coroutines extensions
        }
        androidMain.dependencies {
            // TODO: Add Android driver
        }
        iosMain.dependencies {
            // TODO: Add Native driver
        }
        val desktopMain by getting {
            dependencies {
                // TODO: Add JVM driver
            }
        }
    }
}

// TODO: Configure sqldelight block
// Database name: NotesDatabase
// Package: com.example.notes.data
// Enable async generation