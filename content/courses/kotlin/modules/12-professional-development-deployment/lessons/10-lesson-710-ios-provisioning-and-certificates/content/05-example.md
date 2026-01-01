---
type: "EXAMPLE"
title: "Xcode Project Configuration"
---

Configure signing in your KMP iOS project:

```swift
// In Xcode, select your iOS target and go to Signing & Capabilities

// Option 1: Automatic Signing (Recommended for development)
// - Check "Automatically manage signing"
// - Select your Team
// - Xcode handles certificates and profiles automatically

// Option 2: Manual Signing (Required for CI/CD)
// - Uncheck "Automatically manage signing"
// - Select specific provisioning profile for Debug and Release

// In your project.pbxproj (usually managed by Xcode):
// For manual signing, you'll see:
CODE_SIGN_IDENTITY = "Apple Distribution";
CODE_SIGN_STYLE = Manual;
DEVELOPMENT_TEAM = YOUR_TEAM_ID;
PROVISIONING_PROFILE_SPECIFIER = "Your App Store Profile";

// For KMP projects, the iOS app is in iosApp/ directory
// Open iosApp/iosApp.xcodeproj (or .xcworkspace if using CocoaPods)
```
