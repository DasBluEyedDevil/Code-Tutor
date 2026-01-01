---
type: "THEORY"
title: "Solution 1"
---



---



```kotlin
@Composable
fun AnimatedLikeButton() {
    var isLiked by remember { mutableStateOf(false) }
    var animationTrigger by remember { mutableStateOf(0) }

    val scale by animateFloatAsState(
        targetValue = if (animationTrigger > 0) 1.3f else 1f,
        animationSpec = spring(
            dampingRatio = Spring.DampingRatioMediumBouncy,
            stiffness = Spring.StiffnessLow
        ),
        finishedListener = {
            if (animationTrigger > 0) {
                animationTrigger = 0
            }
        }
    )

    IconButton(
        onClick = {
            isLiked = !isLiked
            animationTrigger++
        }
    ) {
        Icon(
            imageVector = if (isLiked) Icons.Filled.Favorite else Icons.Outlined.FavoriteBorder,
            contentDescription = "Like",
            tint = if (isLiked) Color.Red else Color.Gray,
            modifier = Modifier
                .size(32.dp)
                .scale(scale)
        )
    }
}
```
