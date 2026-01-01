// app/build.gradle.kts
import java.util.Properties

// Load local.properties if it exists
val localProperties = Properties()
val localPropertiesFile = rootProject.file("local.properties")
if (localPropertiesFile.exists()) {
    localProperties.load(localPropertiesFile.inputStream())
}

fun getProperty(envName: String, propName: String): String {
    return System.getenv(envName) 
        ?: localProperties.getProperty(propName) 
        ?: ""
}

android {
    signingConfigs {
        create("release") {
            val keystorePath = getProperty("KEYSTORE_PATH", "signing.keystorePath")
            if (keystorePath.isNotEmpty()) {
                storeFile = file(keystorePath)
            }
            storePassword = getProperty("KEYSTORE_PASSWORD", "signing.keystorePassword")
            keyAlias = getProperty("KEY_ALIAS", "signing.keyAlias")
            keyPassword = getProperty("KEY_PASSWORD", "signing.keyPassword")
        }
    }
    
    buildTypes {
        release {
            isMinifyEnabled = true
            isShrinkResources = true
            proguardFiles(
                getDefaultProguardFile("proguard-android-optimize.txt"),
                "proguard-rules.pro"
            )
            
            // Only use signing config if keystore exists
            val releaseConfig = signingConfigs.findByName("release")
            if (releaseConfig?.storeFile?.exists() == true) {
                signingConfig = releaseConfig
            }
        }
    }
}