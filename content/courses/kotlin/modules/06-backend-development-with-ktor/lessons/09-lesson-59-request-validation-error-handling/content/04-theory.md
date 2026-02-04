---
type: "THEORY"
title: "Building a Validation System"
---


### Step 1: Define Custom Exception Types

Create a hierarchy of exceptions that represent different error conditions:


### Step 2: Create Standardized Error Response Format

Consistent error responses make your API easier to consume:


### Step 3: Build a Validation Framework

Create reusable validation building blocks:


### Step 4: Create Domain-Specific Validators

Now build validators for your specific models:


### Step 5: Integrate Validation into Service Layer

Your service layer is the perfect place to validate:


### Step 6: Handle Errors in Routes with Status Plugins

Install Ktor's StatusPages plugin for global error handling:


Configure the plugin in your application:


### Step 7: Simplify Routes with Error Handling

Now your routes become incredibly clean:


---



```kotlin
// src/main/kotlin/com/example/routes/BookRoutes.kt
package com.example.routes

import com.example.models.ApiResponse
import com.example.models.CreateBookRequest
import com.example.models.UpdateBookRequest
import com.example.services.BookService
import io.ktor.http.*
import io.ktor.server.application.*
import io.ktor.server.request.*
import io.ktor.server.response.*
import io.ktor.server.routing.*

fun Route.bookRoutes(bookService: BookService) {
    route("/api/books") {

        // Get all books
        get {
            bookService.getAllBooks()
                .onSuccess { books ->
                    call.respond(ApiResponse(data = books))
                }
                .onFailure { error ->
                    throw error  // Let StatusPages handle it
                }
        }

        // Get book by ID
        get("/{id}") {
            val id = call.parameters["id"]?.toIntOrNull()
                ?: throw ValidationException("Invalid book ID")

            bookService.getBookById(id)
                .onSuccess { book ->
                    call.respond(ApiResponse(data = book))
                }
                .onFailure { error ->
                    throw error
                }
        }

        // Create new book
        post {
            val request = call.receive<CreateBookRequest>()

            bookService.createBook(request)
                .onSuccess { book ->
                    call.respond(
                        HttpStatusCode.Created,
                        ApiResponse(
                            data = book,
                            message = "Book created successfully"
                        )
                    )
                }
                .onFailure { error ->
                    throw error
                }
        }

        // Update book
        put("/{id}") {
            val id = call.parameters["id"]?.toIntOrNull()
                ?: throw ValidationException("Invalid book ID")
            val request = call.receive<UpdateBookRequest>()

            bookService.updateBook(id, request)
                .onSuccess { book ->
                    call.respond(ApiResponse(
                        data = book,
                        message = "Book updated successfully"
                    ))
                }
                .onFailure { error ->
                    throw error
                }
        }

        // Delete book
        delete("/{id}") {
            val id = call.parameters["id"]?.toIntOrNull()
                ?: throw ValidationException("Invalid book ID")

            bookService.deleteBook(id)
                .onSuccess {
                    call.respond(ApiResponse<Unit>(
                        message = "Book deleted successfully"
                    ))
                }
                .onFailure { error ->
                    throw error
                }
        }
    }
}
```
