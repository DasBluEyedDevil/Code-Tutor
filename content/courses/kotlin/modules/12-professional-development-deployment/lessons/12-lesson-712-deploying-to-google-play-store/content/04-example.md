---
type: "EXAMPLE"
title: "Publishing Your First Release"
---

Step-by-step first release:

```bash
# 1. Build the release bundle
./gradlew :composeApp:bundleRelease
# Output: composeApp/build/outputs/bundle/release/composeApp-release.aab

# 2. Verify the bundle
bundletool validate --bundle composeApp-release.aab

# 3. Check what APKs will be generated
bundletool build-apks \
  --bundle=composeApp-release.aab \
  --output=my_app.apks \
  --mode=universal

# 4. In Play Console:
# a. Go to Release → Production → Create new release
# b. Upload your .aab file
# c. Add release notes
# d. Review and rollout

# For internal testing (faster approval):
# Go to Release → Testing → Internal testing
# Create a release and add testers by email
```
