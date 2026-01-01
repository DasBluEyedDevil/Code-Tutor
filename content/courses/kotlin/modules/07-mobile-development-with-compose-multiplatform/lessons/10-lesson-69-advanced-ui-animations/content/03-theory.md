---
type: "THEORY"
title: "Animation Basics"
---


### animate*AsState

Animate a single value when it changes:


### Multiple Property Animations


### Animation Specs

Control animation duration and easing:


---



```kotlin
val size by animateDpAsState(
    targetValue = if (isExpanded) 200.dp else 100.dp,
    animationSpec = tween(
        durationMillis = 500,
        easing = FastOutSlowInEasing
    )
)

// Spring animation (bouncy)
val offset by animateDpAsState(
    targetValue = if (isVisible) 0.dp else 100.dp,
    animationSpec = spring(
        dampingRatio = Spring.DampingRatioMediumBouncy,
        stiffness = Spring.StiffnessLow
    )
)

// Repeatable animation
val alpha by animateFloatAsState(
    targetValue = if (isHighlighted) 1f else 0.3f,
    animationSpec = repeatable(
        iterations = 3,
        animation = tween(durationMillis = 300),
        repeatMode = RepeatMode.Reverse
    )
)
```
