---
type: "THEORY"
title: "Image Loading with Coil"
---



---



```kotlin
import coil3.compose.AsyncImage
import androidx.compose.foundation.shape.CircleShape
import androidx.compose.ui.draw.clip

@Composable
fun UserAvatar(url: String?, size: Dp = 48.dp) {
    AsyncImage(
        model = url,
        contentDescription = "User avatar",
        modifier = Modifier
            .size(size)
            .clip(CircleShape),
        contentScale = ContentScale.Crop
    )
}

// Usage
@Composable
fun UserCard(user: User) {
    Card(modifier = Modifier.fillMaxWidth()) {
        Row(
            modifier = Modifier.padding(16.dp),
            verticalAlignment = Alignment.CenterVertically
        ) {
            UserAvatar(url = user.avatarUrl)

            Spacer(modifier = Modifier.width(12.dp))

            Column {
                Text(user.name, style = MaterialTheme.typography.titleMedium)
                Text(user.email, style = MaterialTheme.typography.bodySmall)
            }
        }
    }
}
```
