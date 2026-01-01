---
type: "THEORY"
title: "Why Signing Matters"
---

### What is App Signing?

Every Android app must be **digitally signed** before it can be installed on a device or published to the Play Store.

**Signing proves:**
1. **Identity**: You are who you claim to be
2. **Integrity**: The app hasn't been tampered with
3. **Continuity**: Updates come from the same developer

### Key Concepts

| Term | Description |
|------|-------------|
| **Keystore** | A secure file containing your signing keys |
| **Key Alias** | A unique identifier for a key within the keystore |
| **Upload Key** | Key you use to sign apps before uploading to Play Store |
| **App Signing Key** | Key Google uses to sign the final app (Play App Signing) |

### Play App Signing (Recommended)

Google manages your app signing key, and you only manage an upload key:

```
You → Upload Key → Google Play → App Signing Key → Users
```

**Benefits:**
- Google securely stores your app signing key
- If you lose your upload key, you can reset it
- Enables App Bundle optimization
- Required for new apps since August 2021