---
type: "EXAMPLE"
title: "Android App Name Configuration"
---


Update AndroidManifest.xml to use the flavor-specific app name:

**android/app/src/main/AndroidManifest.xml:**



```xml
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
    <application
        android:label="@string/app_name"
        android:icon="@mipmap/ic_launcher">
        <!-- ... rest of manifest ... -->
    </application>
</manifest>

<!-- The @string/app_name will now resolve to the flavor's resValue -->
<!-- dev -> "MyApp Dev" -->
<!-- staging -> "MyApp Staging" -->
<!-- prod -> "MyApp" -->
```
