---
type: "THEORY"
title: "Why Fastlane?"
---

### Manual Deployment Pain Points

- Remembering all the steps
- Certificate management
- Screenshot generation
- Changelog updates
- Human error in signing
- Time-consuming releases

### What Fastlane Automates

| Task | Android | iOS |
|------|---------|-----|
| Building | ✅ Gradle tasks | ✅ xcodebuild |
| Signing | ✅ Keystore | ✅ Certificates + Profiles |
| Screenshots | ✅ screengrab | ✅ snapshot |
| Beta Distribution | ✅ Play Store | ✅ TestFlight |
| Store Metadata | ✅ supply | ✅ deliver |
| Release | ✅ Play Store | ✅ App Store |

### Fastlane Tools

- **match**: Sync certificates across team
- **gym**: Build your app
- **pilot**: Upload to TestFlight
- **deliver**: Upload to App Store
- **supply**: Upload to Play Store
- **snapshot**: Automate screenshots
- **screengrab**: Android screenshots