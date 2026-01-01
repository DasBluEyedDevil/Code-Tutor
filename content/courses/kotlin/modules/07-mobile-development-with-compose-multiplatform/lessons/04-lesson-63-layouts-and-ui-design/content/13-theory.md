---
type: "THEORY"
title: "Solution 3"
---



---



```kotlin
import androidx.compose.foundation.Image
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.grid.GridCells
import androidx.compose.foundation.lazy.grid.LazyVerticalGrid
import androidx.compose.foundation.lazy.grid.items
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.layout.ContentScale
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp

data class Photo(val id: Int, val resourceId: Int)

@Composable
fun PhotoGallery(photos: List<Photo>) {
    LazyVerticalGrid(
        columns = GridCells.Fixed(3),
        contentPadding = PaddingValues(8.dp),
        horizontalArrangement = Arrangement.spacedBy(8.dp),
        verticalArrangement = Arrangement.spacedBy(8.dp)
    ) {
        items(photos, key = { it.id }) { photo ->
            Image(
                painter = painterResource(photo.resourceId),
                contentDescription = "Photo ${photo.id}",
                contentScale = ContentScale.Crop,
                modifier = Modifier
                    .aspectRatio(1f)  // Square
                    .clip(RoundedCornerShape(8.dp))
            )
        }
    }
}

@Preview(showBackground = true)
@Composable
fun PhotoGalleryPreview() {
    val samplePhotos = List(12) { index ->
        Photo(
            id = index,
            resourceId = R.drawable.ic_launcher_foreground
        )
    }

    PhotoGallery(photos = samplePhotos)
}
```
