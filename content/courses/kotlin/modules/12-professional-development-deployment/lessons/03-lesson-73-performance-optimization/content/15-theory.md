---
type: "THEORY"
title: "Solution 3"
---



---



```kotlin
class ImageRepository(
    private val api: ImageApi,
    private val diskCache: DiskLruCache,
    private val memoryCache: LruCache<String, Bitmap>
) {
    private val scope = CoroutineScope(Dispatchers.IO + SupervisorJob())

    // In-flight requests to avoid duplicates
    private val loadingImages = mutableMapOf<String, Deferred<Bitmap>>()

    suspend fun loadImage(url: String): Bitmap? {
        // 1. Check memory cache (fastest)
        memoryCache.get(url)?.let { return it }

        // 2. Check disk cache
        diskCache.get(url)?.let { bytes ->
            val bitmap = BitmapFactory.decodeByteArray(bytes, 0, bytes.size)
            memoryCache.put(url, bitmap)
            return bitmap
        }

        // 3. Coalesce network requests
        return loadingImages[url]?.await() ?: run {
            val deferred = scope.async {
                downloadAndCache(url)
            }
            loadingImages[url] = deferred

            try {
                deferred.await().also {
                    loadingImages.remove(url)
                }
            } catch (e: Exception) {
                loadingImages.remove(url)
                null
            }
        }
    }

    private suspend fun downloadAndCache(url: String): Bitmap {
        val bytes = api.downloadImage(url)
        val bitmap = BitmapFactory.decodeByteArray(bytes, 0, bytes.size)

        // Cache in memory
        memoryCache.put(url, bitmap)

        // Cache on disk
        diskCache.put(url, bytes)

        return bitmap
    }

    fun prefetch(urls: List<String>) {
        scope.launch {
            urls.forEach { url ->
                if (url !in memoryCache && url !in diskCache) {
                    try {
                        loadImage(url)
                    } catch (e: Exception) {
                        // Ignore prefetch errors
                    }
                }
            }
        }
    }

    fun clearCache() {
        memoryCache.evictAll()
        diskCache.delete()
    }
}

// Usage
class ProductListViewModel(private val imageRepo: ImageRepository) : ViewModel() {
    fun loadProducts(products: List<Product>) {
        // Prefetch images for visible products
        val imageUrls = products.take(10).map { it.imageUrl }
        imageRepo.prefetch(imageUrls)
    }
}
```
