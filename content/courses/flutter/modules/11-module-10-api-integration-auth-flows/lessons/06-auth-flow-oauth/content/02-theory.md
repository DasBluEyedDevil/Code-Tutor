---
type: "THEORY"
title: "Section 1: Google Sign-In Setup with Firebase"
---

Google Sign-In requires configuration in both the Firebase Console and your Flutter project. This setup enables your app to authenticate users through their Google accounts.

**Step 1: Configure Firebase Project**

If you do not already have a Firebase project, create one:

1. Go to [Firebase Console](https://console.firebase.google.com/)
2. Click "Add project" and follow the setup wizard
3. Enter your project name and continue
4. You can disable Google Analytics for now (optional)
5. Click "Create project"

**Step 2: Add Your Android App to Firebase**

1. In Firebase Console, click the Android icon to add an Android app
2. Enter your Android package name (found in `android/app/build.gradle` as `applicationId`)
3. Enter an app nickname (optional)
4. Download `google-services.json`
5. Place the file in `android/app/google-services.json`

**Step 3: Generate SHA-1 and SHA-256 Keys**

Google Sign-In requires your app's signing keys registered with Firebase. Generate them:

```bash
# For debug builds (development)
cd android
./gradlew signingReport
```

This outputs something like:

```
Variant: debug
Config: debug
Store: /Users/you/.android/debug.keystore
Alias: AndroidDebugKey
MD5: A1:B2:C3:...
SHA1: D4:E5:F6:... <- Copy this
SHA-256: G7:H8:I9:... <- And this
```

**For release builds**, use your release keystore:

```bash
keytool -list -v -keystore your-release-key.keystore -alias your-alias
```

**Step 4: Add SHA Keys to Firebase**

1. In Firebase Console, go to Project Settings (gear icon)
2. Scroll to "Your apps" and select your Android app
3. Click "Add fingerprint"
4. Add both SHA-1 and SHA-256 fingerprints
5. Download the updated `google-services.json` and replace the old one

**Step 5: Add Your iOS App to Firebase**

1. In Firebase Console, click the iOS icon to add an iOS app
2. Enter your iOS bundle ID (found in Xcode under Runner > General > Bundle Identifier)
3. Enter an app nickname (optional)
4. Download `GoogleService-Info.plist`
5. Open your iOS project in Xcode
6. Drag the plist file into the Runner folder (same level as Info.plist)
7. Make sure "Copy items if needed" is checked
8. Ensure the file is added to the Runner target

**Step 6: Enable Google Sign-In in Firebase**

1. In Firebase Console, go to Authentication
2. Click "Sign-in method" tab
3. Click "Google" provider
4. Toggle "Enable"
5. Select a project support email
6. Click "Save"

**Step 7: Configure iOS URL Scheme**

Open `GoogleService-Info.plist` and find the `REVERSED_CLIENT_ID` value. Then:

1. Open `ios/Runner/Info.plist`
2. Add the URL scheme:

```xml
<key>CFBundleURLTypes</key>
<array>
  <dict>
    <key>CFBundleTypeRole</key>
    <string>Editor</string>
    <key>CFBundleURLSchemes</key>
    <array>
      <string>com.googleusercontent.apps.YOUR-CLIENT-ID</string>
    </array>
  </dict>
</array>
```

Replace `com.googleusercontent.apps.YOUR-CLIENT-ID` with the actual REVERSED_CLIENT_ID from your plist file.

**Step 8: Update Android Build Files**

Update `android/build.gradle` (project-level):

```gradle
buildscript {
    dependencies {
        // Add this line
        classpath 'com.google.gms:google-services:4.4.0'
    }
}
```

Update `android/app/build.gradle` (app-level):

```gradle
plugins {
    id 'com.android.application'
    id 'kotlin-android'
    id 'dev.flutter.flutter-gradle-plugin'
    id 'com.google.gms.google-services' // Add this line
}
```

Your Firebase and Google Sign-In configuration is now complete. In the next section, we will implement the Flutter code.

