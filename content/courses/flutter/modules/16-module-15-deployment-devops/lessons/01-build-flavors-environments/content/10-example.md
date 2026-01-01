---
type: "EXAMPLE"
title: "iOS Schemes Setup"
---


iOS uses Schemes and Build Configurations instead of flavors:

**Step 1: Create Configurations in Xcode**

1. Open `ios/Runner.xcworkspace` in Xcode
2. Click on "Runner" project in navigator
3. Select "Runner" under PROJECT (not TARGET)
4. Go to "Info" tab
5. Under "Configurations", click "+" to duplicate:
   - Duplicate "Debug" -> "Debug-dev", "Debug-staging", "Debug-prod"
   - Duplicate "Release" -> "Release-dev", "Release-staging", "Release-prod"
   - Duplicate "Profile" -> "Profile-dev", "Profile-staging", "Profile-prod"



```swift
// Your Configurations section should look like:
//
// Debug
//   Debug-dev
//   Debug-staging  
//   Debug-prod
// Release
//   Release-dev
//   Release-staging
//   Release-prod
// Profile
//   Profile-dev
//   Profile-staging
//   Profile-prod
```
