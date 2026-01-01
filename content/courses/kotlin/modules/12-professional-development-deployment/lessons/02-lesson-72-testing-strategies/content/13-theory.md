---
type: "THEORY"
title: "Solution 1"
---



**Tests**:

---



```kotlin
// src/test/kotlin/com/example/repository/ProductRepositoryTest.kt
package com.example.repository

import io.mockk.*
import kotlinx.coroutines.test.runTest
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import kotlin.test.*

class ProductRepositoryTest {

    private lateinit var mockApi: ProductApi
    private lateinit var repository: ProductRepository

    @BeforeEach
    fun setup() {
        mockApi = mockk()
        repository = ProductRepository(mockApi)
    }

    @Test
    fun `getProducts should fetch from API and cache results`() = runTest {
        val products = listOf(
            Product("1", "Laptop", 999.99, 10),
            Product("2", "Mouse", 29.99, 50)
        )

        coEvery { mockApi.getProducts() } returns products

        val result = repository.getProducts()

        assertEquals(2, result.size)
        assertEquals("Laptop", result[0].name)

        coVerify(exactly = 1) { mockApi.getProducts() }
    }

    @Test
    fun `getProducts should return cached data when API fails`() = runTest {
        val products = listOf(Product("1", "Laptop", 999.99, 10))

        // First call succeeds
        coEvery { mockApi.getProducts() } returns products
        repository.getProducts()

        // Second call fails
        coEvery { mockApi.getProducts() } throws Exception("Network error")
        val result = repository.getProducts()

        // Should return cached data
        assertEquals(1, result.size)
        assertEquals("Laptop", result[0].name)
    }

    @Test
    fun `getProduct should return cached product if available`() = runTest {
        val product = Product("1", "Laptop", 999.99, 10)

        coEvery { mockApi.getProducts() } returns listOf(product)
        repository.getProducts() // Populate cache

        val result = repository.getProduct("1")

        assertNotNull(result)
        assertEquals("Laptop", result.name)

        // API not called (used cache)
        coVerify(exactly = 0) { mockApi.getProduct(any()) }
    }

    @Test
    fun `getProduct should fetch from API if not cached`() = runTest {
        val product = Product("1", "Laptop", 999.99, 10)

        coEvery { mockApi.getProduct("1") } returns product

        val result = repository.getProduct("1")

        assertNotNull(result)
        assertEquals("Laptop", result.name)

        coVerify(exactly = 1) { mockApi.getProduct("1") }
    }

    @Test
    fun `getProduct should return null when product not found`() = runTest {
        coEvery { mockApi.getProduct("999") } throws Exception("Not found")

        val result = repository.getProduct("999")

        assertNull(result)
    }

    @Test
    fun `createProduct should call API and cache result`() = runTest {
        val newProduct = Product("3", "Keyboard", 79.99, 30)

        coEvery { mockApi.createProduct(newProduct) } returns newProduct

        val result = repository.createProduct(newProduct)

        assertTrue(result.isSuccess)
        assertEquals("Keyboard", result.getOrNull()?.name)

        // Verify cached
        val cached = repository.getProduct("3")
        assertNotNull(cached)
        assertEquals("Keyboard", cached.name)
    }

    @Test
    fun `createProduct should return failure when API fails`() = runTest {
        val newProduct = Product("3", "Keyboard", 79.99, 30)

        coEvery { mockApi.createProduct(newProduct) } throws Exception("Server error")

        val result = repository.createProduct(newProduct)

        assertTrue(result.isFailure)
    }

    @Test
    fun `updateProduct should update cache on success`() = runTest {
        val updated = Product("1", "Gaming Laptop", 1299.99, 5)

        coEvery { mockApi.updateProduct("1", updated) } returns updated

        val result = repository.updateProduct("1", updated)

        assertTrue(result.isSuccess)
        assertEquals("Gaming Laptop", result.getOrNull()?.name)
    }

    @Test
    fun `deleteProduct should remove from cache`() = runTest {
        val product = Product("1", "Laptop", 999.99, 10)

        // Add to cache
        coEvery { mockApi.getProduct("1") } returns product
        repository.getProduct("1")

        // Delete
        coEvery { mockApi.deleteProduct("1") } just Runs

        val result = repository.deleteProduct("1")

        assertTrue(result.isSuccess)

        // Verify removed from cache
        coEvery { mockApi.getProduct("1") } throws Exception("Not found")
        val cached = repository.getProduct("1")
        assertNull(cached)
    }

    @Test
    fun `clearCache should remove all cached products`() = runTest {
        val products = listOf(Product("1", "Laptop", 999.99, 10))

        coEvery { mockApi.getProducts() } returns products
        repository.getProducts()

        repository.clearCache()

        coEvery { mockApi.getProduct("1") } throws Exception("Not found")
        val cached = repository.getProduct("1")
        assertNull(cached)
    }
}
```
