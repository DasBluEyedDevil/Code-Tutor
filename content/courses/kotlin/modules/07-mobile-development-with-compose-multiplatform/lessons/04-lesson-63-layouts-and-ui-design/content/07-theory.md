---
type: "THEORY"
title: "Material Design 3 Theming"
---


### Color Scheme

Material 3 uses a **dynamic color system**:


### Theme Setup


### Typography


### Using Theme


---



```kotlin
@Composable
fun ThemedContent() {
    // Access theme colors
    val backgroundColor = MaterialTheme.colorScheme.background
    val primaryColor = MaterialTheme.colorScheme.primary
    val textColor = MaterialTheme.colorScheme.onBackground

    Column(
        modifier = Modifier
            .fillMaxSize()
            .background(backgroundColor)
    ) {
        // Use theme typography
        Text(
            "Headline",
            style = MaterialTheme.typography.headlineLarge,
            color = MaterialTheme.colorScheme.onBackground
        )

        Text(
            "Body text",
            style = MaterialTheme.typography.bodyMedium
        )

        // Components automatically use theme colors
        Button(onClick = { }) {
            Text("Themed Button")  // Uses primary color
        }
    }
}
```
