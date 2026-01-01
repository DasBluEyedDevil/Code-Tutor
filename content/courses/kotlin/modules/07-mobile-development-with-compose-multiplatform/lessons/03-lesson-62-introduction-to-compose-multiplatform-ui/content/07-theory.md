---
type: "THEORY"
title: "Layout Composables"
---


### Column (Vertical Stack)

Arrange children vertically:


Result:

### Row (Horizontal Stack)

Arrange children horizontally:


Result:

### Nested Layouts

Combine `Column` and `Row`:


---



```kotlin
@Composable
fun ProfileCard() {
    Column {
        Text("John Doe", fontSize = 24.sp, fontWeight = FontWeight.Bold)

        Row {
            Icon(Icons.Default.Email, contentDescription = "Email")
            Text("john@example.com")
        }

        Row {
            Icon(Icons.Default.Phone, contentDescription = "Phone")
            Text("+1 234 567 8900")
        }
    }
}
```
