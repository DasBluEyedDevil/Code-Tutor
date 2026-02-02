---
type: "THEORY"
title: "Introduction"
---


**iOS Code Signing Overview**

Unlike Android's single keystore approach, iOS uses a more complex system involving certificates and provisioning profiles. This system ensures:
- Only authorized developers can build apps
- Only approved devices can run development builds
- Only verified apps can be distributed through the App Store

**Apple Developer Program**

To distribute iOS apps, you need an Apple Developer Program membership:
- **Individual Account:** $99/year - Apps published under your name
- **Organization Account:** $99/year - Apps published under company name, requires D-U-N-S number
- **Enterprise Account:** $299/year - For internal distribution only, not App Store

**What You Get with Membership:**
- Access to App Store Connect for app submission
- Ability to create distribution certificates and profiles
- TestFlight for beta testing (up to 10,000 testers)
- Access to beta OS versions and developer resources

**iOS Signing Components:**

| Component | Purpose |
|-----------|----------|
| Signing Certificate | Proves your identity as a developer |
| App ID | Unique identifier for your app |
| Provisioning Profile | Links certificate, App ID, and devices |
| Entitlements | Declares app capabilities (push, iCloud, etc.) |

**Development vs Distribution:**
- Development builds: Run on registered devices for testing
- Distribution builds: Submit to App Store or distribute via TestFlight/Ad Hoc

