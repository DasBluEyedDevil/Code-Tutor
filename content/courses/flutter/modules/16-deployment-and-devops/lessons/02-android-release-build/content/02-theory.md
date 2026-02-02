---
type: "THEORY"
title: "Keystore Creation"
---


**What is a Keystore?**

A keystore is a secure container holding your signing keys. It contains:
- Your private key (signs the app)
- Your certificate (verifies your identity)
- Protected by passwords

**Creating a Keystore with keytool:**

The `keytool` command comes with Java (JDK). Open a terminal:

```bash
# Windows (PowerShell or CMD)
keytool -genkey -v -keystore my-release-key.jks -keyalg RSA -keysize 2048 -validity 10000 -alias my-key-alias

# macOS/Linux
keytool -genkey -v -keystore ~/my-release-key.jks -keyalg RSA -keysize 2048 -validity 10000 -alias my-key-alias
```

**Parameters explained:**
- `-keystore`: Output file path for the keystore
- `-keyalg RSA`: Use RSA algorithm (standard)
- `-keysize 2048`: Key size in bits (2048 is minimum for Play Store)
- `-validity 10000`: Days the key is valid (~27 years)
- `-alias`: Name to reference this key within the keystore

**You'll be prompted for:**
1. Keystore password (create a strong one)
2. Key password (can be same as keystore password)
3. Your name, organization, city, state, country

