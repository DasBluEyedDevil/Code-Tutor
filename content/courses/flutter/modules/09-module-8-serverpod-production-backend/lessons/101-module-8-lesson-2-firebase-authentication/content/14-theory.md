---
type: "THEORY"
title: "Part 2: Google Sign-In"
---


### Setup Google Sign-In

#### 1. Add Package


Run:

#### 2. Android Configuration

Edit `android/app/build.gradle`:


**Get SHA-1 fingerprint:**

**Add to Firebase Console**:
1. Go to Project Settings → Your apps → Android app
2. Click "Add fingerprint"
3. Paste SHA-1 fingerprint

#### 3. iOS Configuration

Edit `ios/Runner/Info.plist`:


Replace `YOUR-CLIENT-ID` with your client ID from `GoogleService-Info.plist`.

#### 4. Get OAuth Client ID

Download `google-services.json` (Android) and `GoogleService-Info.plist` (iOS) from Firebase Console → Project Settings → Your apps.



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
