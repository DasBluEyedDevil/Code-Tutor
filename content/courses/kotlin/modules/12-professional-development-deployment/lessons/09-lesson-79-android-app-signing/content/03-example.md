---
type: "EXAMPLE"
title: "Creating a Keystore"
---

Generate a keystore using `keytool` (comes with JDK):

```bash
# Generate a new keystore with a signing key
keytool -genkeypair \
  -v \
  -storetype PKCS12 \
  -keystore my-release-key.keystore \
  -alias my-key-alias \
  -keyalg RSA \
  -keysize 2048 \
  -validity 10000

# You'll be prompted for:
# - Keystore password (save this securely!)
# - Key password (can be same as keystore password)
# - Your name, organization, location info

# Verify the keystore contents
keytool -list -v -keystore my-release-key.keystore

# IMPORTANT: Back up your keystore and passwords!
# If you lose them, you cannot update your app.
```
