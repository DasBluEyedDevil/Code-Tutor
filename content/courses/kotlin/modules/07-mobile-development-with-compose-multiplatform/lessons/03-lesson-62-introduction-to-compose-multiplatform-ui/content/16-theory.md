---
type: "THEORY"
title: "Solution 3"
---



---



```kotlin
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.height
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp

@Composable
fun UserList() {
    Column {
        ProfileCard(
            name = "Alice Johnson",
            role = "Kotlin Developer",
            initials = "AJ"
        )

        Spacer(modifier = Modifier.height(8.dp))

        ProfileCard(
            name = "Bob Smith",
            role = "Product Manager",
            initials = "BS"
        )

        Spacer(modifier = Modifier.height(8.dp))

        ProfileCard(
            name = "Carol Williams",
            role = "UX Designer",
            initials = "CW"
        )
    }
}
```
