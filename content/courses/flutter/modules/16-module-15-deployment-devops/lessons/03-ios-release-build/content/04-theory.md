---
type: "THEORY"
title: "Provisioning Profiles"
---


**What is a Provisioning Profile?**

A provisioning profile is a package that links:
- Your signing certificate (who can sign)
- Your App ID (which app)
- Device UDIDs (which devices can run it - development/ad hoc only)
- Entitlements (what the app can do)

**App ID (Bundle Identifier)**

The App ID uniquely identifies your app:
- Format: `com.companyname.appname`
- Must match the bundle identifier in your Flutter project
- Created in Apple Developer Portal or automatically by Xcode
- **Explicit App ID:** Matches one specific app (e.g., `com.example.myapp`)
- **Wildcard App ID:** Matches multiple apps (e.g., `com.example.*`) - limited capabilities

**Profile Types:**

1. **iOS App Development:**
   - For testing on registered devices
   - Includes list of allowed device UDIDs
   - Limited to 100 devices per device type per year

2. **Ad Hoc:**
   - For distributing to specific devices outside App Store
   - Includes list of allowed device UDIDs
   - Good for beta testing without TestFlight

3. **App Store:**
   - For App Store and TestFlight distribution
   - No device list (Apple controls distribution)
   - Required for production releases

4. **Enterprise (In-House):**
   - For Enterprise accounts only
   - Internal distribution without device limits
   - Cannot be used for public distribution

