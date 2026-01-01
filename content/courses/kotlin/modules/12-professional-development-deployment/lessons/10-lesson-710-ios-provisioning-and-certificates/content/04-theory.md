---
type: "THEORY"
title: "Creating App ID and Provisioning Profile"
---

### Create App ID

1. Developer Portal → Identifiers → App IDs
2. Click "+" → Select "App IDs" → Continue
3. Select "App" type
4. Enter description and Bundle ID (e.g., `com.yourcompany.yourapp`)
5. Select capabilities (Push Notifications, Sign in with Apple, etc.)
6. Register

### Create Provisioning Profile

1. Developer Portal → Profiles
2. Click "+"
3. Select profile type:
   - **iOS App Development** - for testing
   - **App Store** - for App Store submission
4. Select your App ID
5. Select certificate(s)
6. For development: select test devices
7. Name and generate the profile
8. Download and double-click to install

### Verify Profiles

```bash
# List installed provisioning profiles
ls ~/Library/MobileDevice/Provisioning\ Profiles/

# View profile details
security cms -D -i ~/Library/MobileDevice/Provisioning\ Profiles/YOUR_PROFILE.mobileprovision
```