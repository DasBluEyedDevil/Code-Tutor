---
type: "THEORY"
title: "Solution 2"
---



---



```kotlin
import androidx.compose.foundation.layout.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Favorite
import androidx.compose.material.icons.outlined.FavoriteBorder
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.material3.Text
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp

@Composable
fun LikeButton() {
    var isLiked by remember { mutableStateOf(false) }
    var likeCount by remember { mutableStateOf(42) }

    Row(
        verticalAlignment = Alignment.CenterVertically,
        horizontalArrangement = Arrangement.Center,
        modifier = Modifier.padding(16.dp)
    ) {
        IconButton(onClick = {
            isLiked = !isLiked
            likeCount = if (isLiked) likeCount + 1 else likeCount - 1
        }) {
            Icon(
                imageVector = if (isLiked) Icons.Filled.Favorite else Icons.Outlined.FavoriteBorder,
                contentDescription = if (isLiked) "Unlike" else "Like",
                tint = if (isLiked) Color.Red else Color.Gray,
                modifier = Modifier.size(32.dp)
            )
        }

        Spacer(modifier = Modifier.width(4.dp))

        Text(
            text = "$likeCount",
            fontSize = 18.sp,
            color = if (isLiked) Color.Red else Color.Gray
        )
    }
}

@Preview(showBackground = true)
@Composable
fun LikeButtonPreview() {
    LikeButton()
}
```
