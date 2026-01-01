---
type: "THEORY"
title: "Gestures"
---


### Clickable with Ripple


### Draggable


### Swipe to Dismiss


### Pointer Input (Advanced)


---



```kotlin
@Composable
fun DoubleTapExample() {
    var isLiked by remember { mutableStateOf(false) }

    Box(
        modifier = Modifier
            .size(200.dp)
            .background(if (isLiked) Color.Red else Color.Gray)
            .pointerInput(Unit) {
                detectTapGestures(
                    onDoubleTap = {
                        isLiked = !isLiked
                    }
                )
            },
        contentAlignment = Alignment.Center
    ) {
        Icon(
            Icons.Default.Favorite,
            contentDescription = null,
            tint = Color.White,
            modifier = Modifier.size(64.dp)
        )
    }
}
```
