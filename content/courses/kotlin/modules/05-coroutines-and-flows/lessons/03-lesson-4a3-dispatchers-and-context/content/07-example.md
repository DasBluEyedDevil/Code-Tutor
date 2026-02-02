---
type: "EXAMPLE"
title: "Complete Example: Image Processing Pipeline"
---

Here's a realistic example of using dispatchers for an image processing pipeline:

```kotlin
import kotlinx.coroutines.*

class ImageProcessor {
    private val scope = CoroutineScope(
        Dispatchers.Main + 
        SupervisorJob() + 
        CoroutineName("ImageProcessor")
    )
    
    suspend fun processImage(url: String): Bitmap {
        // 1. Download on IO dispatcher
        val rawBytes = withContext(Dispatchers.IO) {
            println("Downloading on ${Thread.currentThread().name}")
            downloadImage(url)
        }
        
        // 2. Decode and resize on Default (CPU work)
        val bitmap = withContext(Dispatchers.Default) {
            println("Processing on ${Thread.currentThread().name}")
            val decoded = decodeImage(rawBytes)
            resizeImage(decoded, 800, 600)
        }
        
        // 3. Apply filters on Default
        val filtered = withContext(Dispatchers.Default) {
            println("Filtering on ${Thread.currentThread().name}")
            applyFilters(bitmap, listOf(Grayscale, Sharpen))
        }
        
        return filtered // Return to caller's dispatcher
    }
    
    fun loadImageForUI(url: String, onComplete: (Bitmap) -> Unit) {
        scope.launch { // On Main
            try {
                val result = processImage(url) // Switches internally
                onComplete(result) // Called on Main
            } catch (e: Exception) {
                showError(e)
            }
        }
    }
    
    fun cancel() = scope.cancel()
}
```
