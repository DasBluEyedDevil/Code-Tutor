---
type: "THEORY"
title: "Setup FCM in Flutter"
---


### 1. Add Package


Run:

### 2. Android Configuration

Edit `android/app/src/main/AndroidManifest.xml`:


### 3. iOS Configuration

Edit `ios/Runner/Info.plist`:


Request permission in iOS (done programmatically).



```xml
<dict>
    <!-- Add this -->
    <key>FirebaseAppDelegateProxyEnabled</key>
    <false/>
</dict>
```
