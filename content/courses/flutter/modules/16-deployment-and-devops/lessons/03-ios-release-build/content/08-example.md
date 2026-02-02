---
type: "EXAMPLE"
title: "Building iOS Release from Command Line"
---


Automate iOS release builds for CI/CD:



```bash
# Step 1: Build Flutter iOS release
flutter build ios --release --no-codesign

# This creates the release build but doesn't sign it
# Signing will happen during the archive step in Xcode

# Step 2: For fully automated builds (CI/CD), use xcodebuild
# First, ensure you have certificates installed in Keychain

# Clean and build archive
cd ios
xcodebuild clean archive \
  -workspace Runner.xcworkspace \
  -scheme Runner \
  -configuration Release \
  -archivePath build/Runner.xcarchive \
  -destination 'generic/platform=iOS' \
  CODE_SIGN_IDENTITY="Apple Distribution: Your Company (TEAMID)" \
  DEVELOPMENT_TEAM=YOURTEAMID

# Export IPA from archive
xcodebuild -exportArchive \
  -archivePath build/Runner.xcarchive \
  -exportPath build/ipa \
  -exportOptionsPlist ExportOptions.plist

# The ExportOptions.plist specifies distribution method
# (app-store, ad-hoc, enterprise, or development)
```
