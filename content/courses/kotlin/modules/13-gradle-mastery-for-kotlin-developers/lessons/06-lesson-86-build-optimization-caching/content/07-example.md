---
type: "EXAMPLE"
title: "Profiling Builds"
---


Identify slow parts of your build:



```bash
# Generate build scan (detailed online report)
./gradlew build --scan

# Generate local profile
./gradlew build --profile
open build/reports/profile/

# See task execution times
./gradlew build --info

# Debug configuration issues
./gradlew build --debug

# See dependency resolution
./gradlew build --refresh-dependencies
```
