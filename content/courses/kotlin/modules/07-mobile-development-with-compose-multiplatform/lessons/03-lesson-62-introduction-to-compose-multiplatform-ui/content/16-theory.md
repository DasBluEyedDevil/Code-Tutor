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
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp

@Composable
fun UserList() {
    Column {
        ProfileCard(
            name = "Alice Johnson",
            role = "Android Developer",
            imageRes = R.drawable.ic_launcher_foreground
        )

        Spacer(modifier = Modifier.height(8.dp))

        ProfileCard(
            name = "Bob Smith",
            role = "Product Manager",
            imageRes = R.drawable.ic_launcher_foreground
        )

        Spacer(modifier = Modifier.height(8.dp))

        ProfileCard(
            name = "Carol Williams",
            role = "UX Designer",
            imageRes = R.drawable.ic_launcher_foreground
        )
    }
}

@Preview(showBackground = true)
@Composable
fun UserListPreview() {
    UserList()
}
```
