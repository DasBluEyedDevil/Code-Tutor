---
type: "THEORY"
title: "Canvas Drawing"
---


### Basic Shapes


### Custom Progress Indicator


---



```kotlin
@Composable
fun CircularProgressBar(
    progress: Float,  // 0f to 1f
    modifier: Modifier = Modifier
) {
    Canvas(modifier = modifier.size(120.dp)) {
        val strokeWidth = 12.dp.toPx()

        // Background circle
        drawCircle(
            color = Color.LightGray,
            radius = size.minDimension / 2 - strokeWidth / 2,
            style = Stroke(width = strokeWidth)
        )

        // Progress arc
        drawArc(
            color = Color.Blue,
            startAngle = -90f,
            sweepAngle = 360f * progress,
            useCenter = false,
            style = Stroke(width = strokeWidth, cap = StrokeCap.Round),
            size = Size(
                width = size.minDimension - strokeWidth,
                height = size.minDimension - strokeWidth
            ),
            topLeft = Offset(strokeWidth / 2, strokeWidth / 2)
        )
    }
}

@Composable
fun ProgressDemo() {
    var progress by remember { mutableStateOf(0f) }

    Column(horizontalAlignment = Alignment.CenterHorizontally) {
        CircularProgressBar(progress = progress)

        Slider(
            value = progress,
            onValueChange = { progress = it },
            modifier = Modifier.padding(16.dp)
        )

        Text("${(progress * 100).toInt()}%")
    }
}
```
