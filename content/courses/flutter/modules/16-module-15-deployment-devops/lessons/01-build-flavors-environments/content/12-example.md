---
type: "EXAMPLE"
title: "iOS Info.plist Configuration"
---


**Step 3: Update Info.plist**

Use variables that resolve based on configuration:



```xml
<!-- ios/Runner/Info.plist -->
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>CFBundleDisplayName</key>
    <string>$(BUNDLE_DISPLAY_NAME)</string>
    
    <key>CFBundleIdentifier</key>
    <string>$(PRODUCT_BUNDLE_IDENTIFIER)</string>
    
    <key>CFBundleName</key>
    <string>$(BUNDLE_DISPLAY_NAME)</string>
    
    <!-- Rest of your Info.plist -->
</dict>
</plist>

<!-- The $(VARIABLE) syntax reads from the .xcconfig files -->
```
