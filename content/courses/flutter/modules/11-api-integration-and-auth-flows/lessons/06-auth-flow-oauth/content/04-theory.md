---
type: "THEORY"
title: "Section 3: Apple Sign-In Setup"
---

Apple Sign-In is required by the App Store if your app offers any third-party sign-in options. Apple provides a privacy-focused authentication method that works seamlessly on iOS devices.

**Why Apple Sign-In is Special**

1. **Required by App Store**: If you offer Google Sign-In, you must also offer Apple Sign-In
2. **Hide My Email**: Users can choose to hide their real email address
3. **Cross-Platform**: Works on iOS, macOS, and web
4. **Native Experience**: Uses Face ID or Touch ID on Apple devices
5. **No Password Required**: Authentication uses device biometrics

**Step 1: Enable Sign In with Apple Capability**

1. Open your project in Xcode
2. Select the Runner target
3. Go to "Signing & Capabilities" tab
4. Click "+ Capability"
5. Search for and add "Sign In with Apple"

**Step 2: Configure Apple Developer Portal**

1. Go to [Apple Developer Portal](https://developer.apple.com/account/)
2. Navigate to Certificates, Identifiers & Profiles
3. Select "Identifiers" from the sidebar
4. Find your App ID and click on it
5. Scroll down to "Sign In with Apple"
6. Check the checkbox to enable it
7. Click "Configure" next to Sign In with Apple
8. Set up as a Primary App ID
9. Save your changes

**Step 3: Create a Service ID (for web and Android)**

If you need Apple Sign-In on Android or web:

1. In Apple Developer Portal, go to Identifiers
2. Click the "+" button
3. Select "Services IDs" and continue
4. Enter a description and identifier (e.g., com.yourcompany.yourapp.service)
5. Register the Service ID
6. Click on the newly created Service ID
7. Check "Sign In with Apple"
8. Click "Configure"
9. Set your Primary App ID
10. Add your domains and return URLs
11. Save

**Step 4: Create a Key for Server-Side Verification**

1. In Apple Developer Portal, go to Keys
2. Click the "+" button
3. Enter a key name (e.g., "Sign In with Apple Key")
4. Check "Sign In with Apple"
5. Click "Configure" and select your Primary App ID
6. Click "Continue" then "Register"
7. Download the key file (.p8) - you can only download this once!
8. Note the Key ID - you will need this for Serverpod configuration

**Step 5: Update Info.plist for iOS**

Open `ios/Runner/Info.plist` and ensure you have:

```xml
<!-- Add if not present -->
<key>CFBundleURLTypes</key>
<array>
  <dict>
    <key>CFBundleTypeRole</key>
    <string>Editor</string>
    <key>CFBundleURLSchemes</key>
    <array>
      <!-- Your existing URL schemes -->
    </array>
  </dict>
</array>
```

**Step 6: Create Entitlements File**

Ensure `ios/Runner/Runner.entitlements` contains:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>com.apple.developer.applesignin</key>
    <array>
        <string>Default</string>
    </array>
</dict>
</plist>
```

**Understanding Apple Sign-In Data**

When a user signs in with Apple, you receive:

1. **User Identifier**: A unique, stable ID for this user (persists across app reinstalls)
2. **Email**: The user's email or a private relay address if they chose "Hide My Email"
3. **Full Name**: User's name components (first name, last name) - only provided on first sign-in!

**Important**: Apple only provides the user's name on the first sign-in. You must save it immediately because subsequent sign-ins will not include the name. This is a common source of bugs in apps.

Your Apple Sign-In configuration is complete. In the next section, we will implement the Flutter code.

