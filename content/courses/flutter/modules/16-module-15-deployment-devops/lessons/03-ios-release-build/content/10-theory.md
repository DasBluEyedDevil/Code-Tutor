---
type: "THEORY"
title: "Automatic Signing"
---


**Xcode Automatic Signing**

Xcode can automatically manage certificates and provisioning profiles for you:
- Creates certificates when needed
- Generates provisioning profiles
- Registers devices automatically
- Updates profiles when entitlements change

**Enabling Automatic Signing:**

1. Open `ios/Runner.xcworkspace` in Xcode
2. Select the Runner project in the navigator
3. Select the Runner target
4. Go to "Signing & Capabilities" tab
5. Check "Automatically manage signing"
6. Select your Team from the dropdown

**When to Use Automatic Signing:**
- Individual developers
- Small teams with simple apps
- Development and testing
- When you want Xcode to handle everything

**When to Use Manual Signing:**
- CI/CD pipelines
- Large teams with specific profile requirements
- Apps with complex entitlements
- Enterprise distribution
- When you need precise control over signing

