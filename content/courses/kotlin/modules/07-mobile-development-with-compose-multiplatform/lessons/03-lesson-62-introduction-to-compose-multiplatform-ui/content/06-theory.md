---
type: "THEORY"
title: "Basic UI Components"
---


### Text

Display text on screen:


**Units**:
- `sp` (scaled pixels): For text size (respects accessibility settings)
- `dp` (density-independent pixels): For sizes, padding, margins

### Button

Interactive button with click handling:


**Button variations**:


### Image

Display images from resources or URLs:


**Content Scales**:
- `ContentScale.Fit`: Fit entire image (may have empty space)
- `ContentScale.Crop`: Fill entire area (may crop image)
- `ContentScale.FillWidth`: Fill width, maintain aspect ratio
- `ContentScale.FillHeight`: Fill height, maintain aspect ratio

### Icon

Material icons for common UI elements:


**Popular icons**:
- `Icons.Default.Home`
- `Icons.Default.Settings`
- `Icons.Default.Favorite`
- `Icons.Default.Search`
- `Icons.Default.Menu`
- `Icons.Default.Person`
- `Icons.Default.ShoppingCart`

---



```kotlin
@Composable
fun IconExamples() {
    Row {
        Icon(
            imageVector = Icons.Default.Home,
            contentDescription = "Home"
        )

        Icon(
            imageVector = Icons.Default.Favorite,
            contentDescription = "Favorite",
            tint = Color.Red
        )

        Icon(
            imageVector = Icons.Default.Settings,
            contentDescription = "Settings",
            modifier = Modifier.size(48.dp)
        )
    }
}
```
