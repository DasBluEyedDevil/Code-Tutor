---
type: "THEORY"
title: "Infinite Animations"
---



---



```kotlin
@Composable
fun LoadingSpinner() {
    val infiniteTransition = rememberInfiniteTransition(label = "infinite")

    val rotation by infiniteTransition.animateFloat(
        initialValue = 0f,
        targetValue = 360f,
        animationSpec = infiniteRepeatable(
            animation = tween(durationMillis = 1000, easing = LinearEasing),
            repeatMode = RepeatMode.Restart
        ),
        label = "rotation"
    )

    Icon(
        Icons.Default.Refresh,
        contentDescription = "Loading",
        modifier = Modifier
            .size(48.dp)
            .rotate(rotation)
    )
}

@Composable
fun PulsingHeart() {
    val infiniteTransition = rememberInfiniteTransition(label = "pulse")

    val scale by infiniteTransition.animateFloat(
        initialValue = 1f,
        targetValue = 1.3f,
        animationSpec = infiniteRepeatable(
            animation = tween(600),
            repeatMode = RepeatMode.Reverse
        ),
        label = "scale"
    )

    Icon(
        Icons.Default.Favorite,
        contentDescription = "Heart",
        tint = Color.Red,
        modifier = Modifier
            .size(48.dp)
            .scale(scale)
    )
}
```
