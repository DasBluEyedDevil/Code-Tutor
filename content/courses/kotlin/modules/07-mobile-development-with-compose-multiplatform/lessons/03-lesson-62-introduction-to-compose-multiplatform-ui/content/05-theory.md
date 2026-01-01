---
type: "THEORY"
title: "Preview Annotations"
---


### @Preview Basics

The `@Preview` annotation lets you see composables without running the app:


Click the **Preview** tab (right side of editor) to see the UI instantly.

### Preview Parameters


### Multiple Previews

Preview the same composable in different scenarios:


### Interactive Preview

Click the **Interactive Mode** button in preview to:
- Click buttons
- Type in text fields
- Test interactions without running the app

---



```kotlin
@Preview(name = "Light Mode", showBackground = true)
@Preview(name = "Large Text", showBackground = true, fontScale = 2f)
@Preview(name = "Small Screen", widthDp = 360, heightDp = 640)
@Composable
fun MultiPreview() {
    WelcomeMessage()
}
```
