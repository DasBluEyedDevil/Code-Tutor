---
type: "THEORY"
title: "Step 2: iOS Configuration (Universal Links)"
---


### A. Update Info.plist

For Flutter 3.27+, deep linking is enabled by default. For earlier versions:


### B. Enable Associated Domains in Xcode

1. Open `ios/Runner.xcworkspace` in Xcode
2. Select your project in the navigator
3. Go to "Signing & Capabilities" tab
4. Click "+ Capability"
5. Add "Associated Domains"
6. Add domain: `applinks:mycompany.com`

### C. Create apple-app-site-association

Host this file at: `https://mycompany.com/.well-known/apple-app-site-association`


**To find your Team ID:**
1. Open Xcode
2. Go to project settings
3. Look at "Team" field (10-character string)



```json
{
  "applinks": {
    "apps": [],
    "details": [
      {
        "appID": "TEAM_ID.com.mycompany.myapp",
        "paths": ["*"]
      }
    ]
  }
}
```
