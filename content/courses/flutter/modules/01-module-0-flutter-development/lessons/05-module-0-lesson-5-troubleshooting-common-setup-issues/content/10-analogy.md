---
type: "ANALOGY"
title: "Problem 8: Gradle Build Fails (Android)"
---


### Error message:

### Solution 1: Update Gradle

Edit `android/build.gradle`:


### Solution 2: Clear Gradle Cache


### Solution 3: Update Java Version

Flutter requires Java 11 or higher:




```bash
# Check Java version
java -version

# If it's older than 11, download from:
# https://adoptium.net/
```
