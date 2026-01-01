---
type: "THEORY"
title: "Required GitHub Secrets"
---

### Android Secrets

| Secret Name | Description | How to Get |
|-------------|-------------|------------|
| `ANDROID_KEYSTORE_BASE64` | Base64-encoded keystore | `base64 release.keystore` |
| `ANDROID_KEYSTORE_PASSWORD` | Keystore password | Your password |
| `ANDROID_KEY_ALIAS` | Key alias name | Your alias |
| `ANDROID_KEY_PASSWORD` | Key password | Your password |
| `PLAY_STORE_SERVICE_ACCOUNT` | Google Play API JSON | Play Console → API Access |

### iOS Secrets

| Secret Name | Description | How to Get |
|-------------|-------------|------------|
| `IOS_CERTIFICATE_BASE64` | Base64-encoded .p12 | Export from Keychain |
| `IOS_CERTIFICATE_PASSWORD` | Certificate password | Password when exporting |
| `IOS_PROVISIONING_PROFILE_BASE64` | Base64-encoded profile | Developer Portal |
| `KEYCHAIN_PASSWORD` | CI keychain password | Any secure password |
| `APP_STORE_API_KEY_ID` | App Store Connect key ID | App Store Connect |
| `APP_STORE_ISSUER_ID` | App Store Connect issuer | App Store Connect |
| `APP_STORE_API_KEY` | Base64-encoded .p8 key | App Store Connect |