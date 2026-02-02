---
type: "EXAMPLE"
title: "Fixing Entitlement Mismatches"
---


Entitlements must match between your app, App ID, and provisioning profile:



```xml
<!-- ios/Runner/Runner.entitlements -->
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <!-- Push Notifications -->
    <key>aps-environment</key>
    <string>production</string>  <!-- or 'development' for debug -->
    
    <!-- Associated Domains (for Universal Links) -->
    <key>com.apple.developer.associated-domains</key>
    <array>
        <string>applinks:example.com</string>
    </array>
    
    <!-- Sign in with Apple -->
    <key>com.apple.developer.applesignin</key>
    <array>
        <string>Default</string>
    </array>
    
    <!-- App Groups (for sharing data with extensions) -->
    <key>com.apple.security.application-groups</key>
    <array>
        <string>group.com.example.myapp</string>
    </array>
    
    <!-- iCloud -->
    <key>com.apple.developer.icloud-container-identifiers</key>
    <array>
        <string>iCloud.com.example.myapp</string>
    </array>
</dict>
</plist>

<!-- 
Common Entitlement Errors:

1. "Entitlement not allowed" 
   -> Enable capability in Apple Developer Portal for your App ID
   -> Regenerate provisioning profile

2. "Entitlement value not allowed"
   -> Check exact format (e.g., aps-environment must be 'development' or 'production')
   
3. "Missing entitlement"
   -> Add capability in Xcode: Signing & Capabilities -> + Capability
-->
```
