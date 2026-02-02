// shared/build.gradle.kts
plugins {
    kotlin("multiplatform")
    id("com.android.library")
    id("app.cash.sqldelight")
}

kotlin {
    androidTarget()
    iosX64()
    iosArm64()
    iosSimulatorArm64()
    jvm("desktop")
    
    sourceSets {
        commonMain.dependencies {
            implementation("app.cash.sqldelight:coroutines-extensions:2.0.2")
        }
        androidMain.dependencies {
            implementation("app.cash.sqldelight:android-driver:2.0.2")
        }
        iosMain.dependencies {
            implementation("app.cash.sqldelight:native-driver:2.0.2")
        }
        val desktopMain by getting {
            dependencies {
                implementation("app.cash.sqldelight:sqlite-driver:2.0.2")
            }
        }
    }
}

sqldelight {
    databases {
        create("NotesDatabase") {
            packageName.set("com.example.notes.data")
            generateAsync.set(true)
        }
    }
}