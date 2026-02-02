---
type: "EXAMPLE"
title: "Fixing Version Code Issues"
---


Play Store requires version code to increase with each upload:



```yaml
# pubspec.yaml
name: my_app
description: My Flutter App
version: 1.2.3+10  # version name + version code

# Format: major.minor.patch+buildNumber
# 1.2.3 = versionName (shown to users)
# 10 = versionCode (internal, must always increase)

# Increment for each release:
# 1.0.0+1  -> First release
# 1.0.1+2  -> Bug fix
# 1.1.0+3  -> New feature
# 2.0.0+4  -> Major update

# In CI/CD, often calculated automatically:
# versionCode = timestamp or build number
version: 1.2.3+${BUILD_NUMBER}
```
