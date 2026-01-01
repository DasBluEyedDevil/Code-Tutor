// android/app/build.gradle
// Add this inside the android { } block

android {
    // ... existing config ...
    
    flavorDimensions "environment"
    
    productFlavors {
        dev {
            dimension "environment"
            applicationIdSuffix ".dev"
            versionNameSuffix "-dev"
            resValue "string", "app_name", "MyApp Dev"
        }
        
        staging {
            dimension "environment"
            applicationIdSuffix ".staging"
            versionNameSuffix "-staging"
            resValue "string", "app_name", "MyApp Staging"
        }
        
        prod {
            dimension "environment"
            // No applicationIdSuffix for production
            // No versionNameSuffix for production
            resValue "string", "app_name", "MyApp"
        }
    }
}

// Don't forget to update AndroidManifest.xml:
// android:label="@string/app_name"