---
type: "EXAMPLE"
title: "Android Flavors Setup"
---


For different app IDs, names, and icons per environment, configure Android flavors:

**android/app/build.gradle:**



```groovy
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
            // No suffix for production
            resValue "string", "app_name", "MyApp"
        }
    }
}

// This creates app IDs like:
// dev: com.mycompany.myapp.dev
// staging: com.mycompany.myapp.staging  
// prod: com.mycompany.myapp
```
