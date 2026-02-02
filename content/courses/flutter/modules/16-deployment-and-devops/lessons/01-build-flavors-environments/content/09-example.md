---
type: "EXAMPLE"
title: "Android Flavor-Specific Icons"
---


Create separate icon directories for each flavor:

**Directory structure:**
```
android/app/src/
├── main/
│   └── res/
│       └── mipmap-*/
│           └── ic_launcher.png  (fallback)
├── dev/
│   └── res/
│       └── mipmap-*/
│           └── ic_launcher.png  (dev icon - maybe with DEBUG banner)
├── staging/
│   └── res/
│       └── mipmap-*/
│           └── ic_launcher.png  (staging icon - maybe orange tint)
└── prod/
    └── res/
        └── mipmap-*/
            └── ic_launcher.png  (production icon)
```

**Tip:** Use different colored banners or badges on dev/staging icons so testers instantly know which version they're using.



```dart
// You can also use flavor-specific resources for other assets:
// android/app/src/dev/res/values/strings.xml
// android/app/src/staging/res/values/strings.xml
// android/app/src/prod/res/values/strings.xml

// Each flavor can have its own:
// - App icons
// - Splash screens  
// - String resources
// - Firebase config files (google-services.json)
```
