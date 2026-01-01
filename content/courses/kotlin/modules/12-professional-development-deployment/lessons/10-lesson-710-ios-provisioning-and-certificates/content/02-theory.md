---
type: "THEORY"
title: "iOS Code Signing Overview"
---

### Why iOS Signing is More Complex

Apple requires multiple layers of trust:

```
Apple → Developer Certificate → Provisioning Profile → Your App → Device
```

### Key Components

| Component | Purpose | Created Where |
|-----------|---------|---------------|
| **Apple Developer Account** | Identity with Apple | developer.apple.com |
| **Certificate** | Proves you're a registered developer | Keychain + Apple |
| **App ID** | Unique identifier for your app | Developer Portal |
| **Provisioning Profile** | Links certificate + app ID + devices | Developer Portal |

### Certificate Types

| Type | Use Case |
|------|----------|
| **Development** | Testing on your devices |
| **Distribution (App Store)** | Submitting to App Store |
| **Distribution (Ad Hoc)** | Testing on specific devices |
| **Distribution (Enterprise)** | In-house distribution |