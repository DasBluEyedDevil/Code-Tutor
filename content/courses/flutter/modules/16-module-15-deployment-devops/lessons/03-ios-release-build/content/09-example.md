---
type: "EXAMPLE"
title: "ExportOptions.plist Configuration"
---


Create an ExportOptions.plist for automated exports:



```xml
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <!-- Distribution method: app-store, ad-hoc, enterprise, development -->
    <key>method</key>
    <string>app-store</string>
    
    <!-- Your Apple Developer Team ID -->
    <key>teamID</key>
    <string>ABCD1234EF</string>
    
    <!-- Upload to App Store Connect after export -->
    <key>destination</key>
    <string>upload</string>
    
    <!-- For app-store, this should be true -->
    <key>uploadSymbols</key>
    <true/>
    
    <!-- Provisioning profile to use -->
    <key>provisioningProfiles</key>
    <dict>
        <key>com.example.myapp</key>
        <string>My App Store Profile</string>
    </dict>
    
    <!-- For ad-hoc distribution, set to false -->
    <!-- <key>compileBitcode</key> -->
    <!-- <false/> -->
</dict>
</plist>
```
