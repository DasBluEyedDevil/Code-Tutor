// android/app/build.gradle

plugins {
    id "com.android.application"
    id "kotlin-android"
    id "dev.flutter.flutter-gradle-plugin"
}

// TODO: Load key.properties file safely
// Hint: Check if file exists before loading

android {
    namespace = "com.example.myapp"
    compileSdk = flutter.compileSdkVersion

    defaultConfig {
        applicationId = "com.example.myapp"
        minSdk = flutter.minSdkVersion
        targetSdk = flutter.targetSdkVersion
        versionCode = flutter.versionCode
        versionName = flutter.versionName
    }

    // TODO: Add signingConfigs block with 'release' config
    // Read values from keystoreProperties

    buildTypes {
        debug {
            signingConfig signingConfigs.debug
        }
        // TODO: Add release build type
        // - Use release signing config
        // - Enable minifyEnabled
        // - Enable shrinkResources
        // - Add proguard files
    }
}