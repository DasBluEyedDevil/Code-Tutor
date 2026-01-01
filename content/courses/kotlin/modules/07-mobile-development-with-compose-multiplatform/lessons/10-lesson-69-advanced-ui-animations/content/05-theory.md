---
type: "THEORY"
title: "Transitions"
---


### updateTransition

Coordinate multiple animations:


---



```kotlin
enum class BoxState { Small, Large }

@Composable
fun TransitionExample() {
    var currentState by remember { mutableStateOf(BoxState.Small) }

    val transition = updateTransition(targetState = currentState, label = "box")

    val size by transition.animateDp(label = "size") { state ->
        when (state) {
            BoxState.Small -> 100.dp
            BoxState.Large -> 200.dp
        }
    }

    val color by transition.animateColor(label = "color") { state ->
        when (state) {
            BoxState.Small -> Color.Blue
            BoxState.Large -> Color.Red
        }
    }

    val cornerRadius by transition.animateDp(label = "cornerRadius") { state ->
        when (state) {
            BoxState.Small -> 8.dp
            BoxState.Large -> 50.dp
        }
    }

    Box(
        modifier = Modifier
            .size(size)
            .background(color, RoundedCornerShape(cornerRadius))
            .clickable {
                currentState = if (currentState == BoxState.Small) {
                    BoxState.Large
                } else {
                    BoxState.Small
                }
            }
    )
}
```
