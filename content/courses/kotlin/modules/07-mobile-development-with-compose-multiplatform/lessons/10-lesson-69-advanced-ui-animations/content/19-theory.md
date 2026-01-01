---
type: "THEORY"
title: "Animations on iOS"
---


### Cross-Platform Animations

All Compose animations work identically on Android and iOS! The same animation code runs natively on both platforms.

| Animation Type | Android | iOS |
|----------------|---------|-----|
| **animate*AsState** | Same code | Same code |
| **AnimatedVisibility** | Same code | Same code |
| **updateTransition** | Same code | Same code |
| **Canvas drawing** | Same code | Same code |
| **Gestures** | Same code | Same code |

### Platform-Specific Considerations

**Haptic Feedback**: iOS has more nuanced haptic feedback
- Consider using expect/actual for haptics
- iOS users expect subtle haptic responses

**Animation Curves**: Both platforms support the same easing curves, but users may have different expectations:
- iOS: Often uses spring animations
- Android: Often uses standard easing

### Running Animations on iOS

1. Build and run on iOS Simulator
2. Test all animations - they run natively!
3. Verify gestures feel natural on iOS
4. Check performance is smooth (60fps)

```kotlin
// This animation code works on BOTH platforms!
@Composable
fun CrossPlatformAnimation() {
    var expanded by remember { mutableStateOf(false) }
    
    val size by animateDpAsState(
        targetValue = if (expanded) 200.dp else 100.dp,
        animationSpec = spring(
            dampingRatio = Spring.DampingRatioMediumBouncy
        )
    )
    
    Box(
        Modifier
            .size(size)
            .background(MaterialTheme.colorScheme.primary)
            .clickable { expanded = !expanded }
    )
}
```

---

