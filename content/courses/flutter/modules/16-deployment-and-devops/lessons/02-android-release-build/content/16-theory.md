---
type: "THEORY"
title: "Common Build Issues"
---


**1. Keystore Not Found**

```
Execution failed for task ':app:validateSigningRelease'.
> Keystore file not found for signing config 'release'.
```

**Solution:** Check storeFile path in key.properties. Use absolute path or correct relative path from android/ folder.

**2. Wrong Password**

```
java.io.IOException: Keystore was tampered with, or password was incorrect
```

**Solution:** Verify passwords in key.properties match what you set during keystore creation.

**3. Missing key.properties**

```
Could not read key.properties
```

**Solution:** Create key.properties file in android/ folder. Ensure the file exists and is readable.

