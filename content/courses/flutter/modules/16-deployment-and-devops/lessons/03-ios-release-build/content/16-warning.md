---
type: "WARNING"
title: "Pre-Submission Checklist for iOS"
---


**Before Uploading to App Store Connect:**

1. **Bundle Identifier:**
   - Matches your App ID in Apple Developer Portal
   - Matches the app record in App Store Connect
   - Check: `ios/Runner.xcodeproj` -> Runner target -> General

2. **Version and Build Numbers:**
   - Version (CFBundleShortVersionString): User-visible version (1.2.3)
   - Build (CFBundleVersion): Must be unique for each upload (can be 1, 2, 3...)
   - Flutter uses: `version: 1.2.3+4` in pubspec.yaml (1.2.3 = version, 4 = build)

3. **Signing Configuration:**
   - Using App Store distribution profile
   - Correct team selected
   - No "Missing entitlements" warnings

4. **App Icons:**
   - All required sizes present (1024x1024 for App Store)
   - No alpha channel in App Store icon
   - Check: `ios/Runner/Assets.xcassets/AppIcon.appiconset`

5. **Launch Screen:**
   - Configured properly in `ios/Runner/Base.lproj/LaunchScreen.storyboard`
   - No placeholder images

6. **Required Permissions:**
   - Privacy descriptions in Info.plist for all used permissions
   - Camera, photos, location, etc. must have usage descriptions

7. **Validate in Xcode:**
   - Always run "Validate App" before "Distribute App"
   - Fix all errors and warnings

