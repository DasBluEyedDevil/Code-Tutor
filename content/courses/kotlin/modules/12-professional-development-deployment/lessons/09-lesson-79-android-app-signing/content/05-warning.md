---
type: "WARNING"
title: "Keystore Security Best Practices"
---

### NEVER Do This:

```kotlin
// ❌ NEVER hardcode credentials in build.gradle.kts
signingConfigs {
    create("release") {
        storeFile = file("release.keystore")
        storePassword = "my_password"  // NEVER!
        keyAlias = "my_alias"
        keyPassword = "my_password"  // NEVER!
    }
}
```

### Secure Storage Options:

**1. Environment Variables (CI/CD):**
```bash
export KEYSTORE_PASSWORD=your_password
export KEY_PASSWORD=your_password
```

**2. local.properties (Local Development):**
```properties
# local.properties (in .gitignore)
KEYSTORE_PATH=/path/to/keystore
KEYSTORE_PASSWORD=your_password
KEY_ALIAS=your_alias
KEY_PASSWORD=your_password
```

**3. GitHub Secrets (GitHub Actions):**
```yaml
# Base64 encode your keystore
cat my-release-key.keystore | base64 > keystore_base64.txt
# Add to GitHub Secrets as KEYSTORE_BASE64
```

### Backup Checklist:
- [ ] Keystore file backed up in 2+ secure locations
- [ ] Passwords stored in password manager
- [ ] Documented recovery procedure
- [ ] Team members know the process