---
type: "EXAMPLE"
title: "Configuring Signing in Xcode Project"
---


Flutter's default iOS configuration in Xcode:



```text
// ios/Runner.xcodeproj/project.pbxproj
// These settings are configured in Xcode UI,
// but you can also edit directly:

// For automatic signing (development):
CODE_SIGN_IDENTITY = "Apple Development";
CODE_SIGN_STYLE = Automatic;
DEVELOPMENT_TEAM = YOURTEAMID;
PROVISIONING_PROFILE_SPECIFIER = "";

// For manual signing (recommended for release):
CODE_SIGN_IDENTITY = "Apple Distribution";
CODE_SIGN_STYLE = Manual;
DEVELOPMENT_TEAM = YOURTEAMID;
PROVISIONING_PROFILE_SPECIFIER = "My App Store Profile";

// Bundle identifier (must match App ID)
PRODUCT_BUNDLE_IDENTIFIER = com.example.myapp;

// You can configure different settings per build configuration:
// Debug: Automatic signing with development certificate
// Release: Manual signing with distribution certificate
```
