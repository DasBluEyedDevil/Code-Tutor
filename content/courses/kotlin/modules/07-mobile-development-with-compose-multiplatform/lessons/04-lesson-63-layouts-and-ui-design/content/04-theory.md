---
type: "THEORY"
title: "Arrangement and Alignment"
---


### Column Arrangement

Control vertical spacing:


### Column Alignment

Control horizontal alignment of children:


### Row Arrangement and Alignment


---



```kotlin
@Composable
fun RowLayouts() {
    // Horizontal arrangement
    Row(horizontalArrangement = Arrangement.SpaceBetween) {
        Text("Left")
        Text("Right")
    }

    // Vertical alignment
    Row(verticalAlignment = Alignment.Top) { }
    Row(verticalAlignment = Alignment.CenterVertically) { }
    Row(verticalAlignment = Alignment.Bottom) { }
}
```
