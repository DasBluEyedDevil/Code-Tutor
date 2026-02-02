---
type: "EXAMPLE"
title: "iOS Schemes Creation"
---


**Step 2: Create Schemes**

1. In Xcode: Product -> Scheme -> Manage Schemes
2. Click "+" to add schemes:
   - "dev" -> Build Configuration: Debug-dev
   - "staging" -> Build Configuration: Debug-staging
   - "prod" -> Build Configuration: Debug-prod
3. For each scheme, edit and set configurations:
   - Run: Debug-{flavor}
   - Test: Debug-{flavor}
   - Profile: Profile-{flavor}
   - Analyze: Debug-{flavor}
   - Archive: Release-{flavor}



```swift
// ios/Flutter/Dev.xcconfig
#include "Generated.xcconfig"
BUNDLE_DISPLAY_NAME = MyApp Dev
PRODUCT_BUNDLE_IDENTIFIER = com.mycompany.myapp.dev

// ios/Flutter/Staging.xcconfig  
#include "Generated.xcconfig"
BUNDLE_DISPLAY_NAME = MyApp Staging
PRODUCT_BUNDLE_IDENTIFIER = com.mycompany.myapp.staging

// ios/Flutter/Prod.xcconfig
#include "Generated.xcconfig"
BUNDLE_DISPLAY_NAME = MyApp
PRODUCT_BUNDLE_IDENTIFIER = com.mycompany.myapp
```
