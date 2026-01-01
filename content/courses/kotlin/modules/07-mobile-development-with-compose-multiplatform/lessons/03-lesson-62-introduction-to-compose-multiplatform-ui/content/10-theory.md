---
type: "THEORY"
title: "Putting It All Together"
---


### Profile Card Example


### Interactive Counter App


---



```kotlin
@Composable
fun CounterApp() {
    var count by remember { mutableStateOf(0) }

    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(16.dp),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ) {
        Text(
            text = "Count: $count",
            fontSize = 48.sp,
            fontWeight = FontWeight.Bold
        )

        Spacer(modifier = Modifier.height(16.dp))

        Row(horizontalArrangement = Arrangement.spacedBy(8.dp)) {
            Button(onClick = { count-- }) {
                Text("-")
            }

            Button(onClick = { count = 0 }) {
                Text("Reset")
            }

            Button(onClick = { count++ }) {
                Text("+")
            }
        }
    }
}

@Preview(showBackground = true)
@Composable
fun CounterAppPreview() {
    CounterApp()
}
```
