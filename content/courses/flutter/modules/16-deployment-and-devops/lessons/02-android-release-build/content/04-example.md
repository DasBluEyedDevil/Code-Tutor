---
type: "EXAMPLE"
title: "Setting Up key.properties"
---


Create a `key.properties` file to store keystore references securely:

**android/key.properties:**



```properties
# Store this file outside of version control!
# Add to .gitignore: android/key.properties

storePassword=your_keystore_password
keyPassword=your_key_password
keyAlias=my-key-alias
storeFile=C:/Users/yourname/keystores/my-release-key.jks

# On macOS/Linux:
# storeFile=/Users/yourname/keystores/my-release-key.jks

# Or relative to android/ folder:
# storeFile=../keystores/my-release-key.jks
```
