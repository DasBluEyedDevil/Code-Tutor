---
type: "THEORY"
title: "Phase 2: Backend Development (4-6 hours)"
---


### 2.1 Database Schema


### 2.2 Models


### 2.3 Core Services


### 2.4 API Routes


---



```kotlin
// src/main/kotlin/com/shopkotlin/routes/productRoutes.kt
package com.shopkotlin.routes

import com.shopkotlin.services.ProductService
import io.ktor.server.application.*
import io.ktor.server.response.*
import io.ktor.server.routing.*
import io.ktor.http.*

fun Route.productRoutes(productService: ProductService) {
    route("/api/products") {
        get {
            val category = call.parameters["category"]
            val search = call.parameters["search"]
            val featured = call.parameters["featured"]?.toBoolean()
            val limit = call.parameters["limit"]?.toIntOrNull() ?: 50
            val offset = call.parameters["offset"]?.toIntOrNull() ?: 0

            val products = when {
                search != null -> productService.search(search, limit, offset)
                category != null -> productService.getByCategory(category, limit, offset)
                featured == true -> productService.getFeatured(limit)
                else -> productService.getAll(limit, offset)
            }

            call.respond(ApiResponse(success = true, data = products))
        }

        get("/{id}") {
            val id = call.parameters["id"]
                ?: return@get call.respond(
                    HttpStatusCode.BadRequest,
                    ApiResponse<Unit>(success = false, message = "Product ID required")
                )

            val product = productService.getById(id)
                ?: return@get call.respond(
                    HttpStatusCode.NotFound,
                    ApiResponse<Unit>(success = false, message = "Product not found")
                )

            call.respond(ApiResponse(success = true, data = product))
        }
    }

    route("/api/categories") {
        get {
            val categories = productService.getAllCategories()
            call.respond(ApiResponse(success = true, data = categories))
        }
    }
}
```
