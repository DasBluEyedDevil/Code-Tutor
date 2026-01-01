---
type: "THEORY"
title: "Solution"
---



---



```kotlin
// src/test/kotlin/com/example/services/PostServiceTest.kt
package com.example.services

import com.example.models.Post
import com.example.models.CreatePostRequest
import com.example.plugins.UserPrincipal
import com.example.repositories.PostRepository
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import kotlin.test.*

class MockPostRepository : PostRepository {
    private val posts = mutableMapOf<Int, Post>()
    private var nextId = 1

    override fun insert(title: String, content: String, authorId: Int): Int {
        val id = nextId++
        posts[id] = Post(
            id = id,
            title = title,
            content = content,
            authorId = authorId,
            authorName = "Test User",
            createdAt = "2025-01-01T00:00:00"
        )
        return id
    }

    override fun update(id: Int, title: String, content: String): Boolean {
        val post = posts[id] ?: return false
        posts[id] = post.copy(title = title, content = content)
        return true
    }

    override fun delete(id: Int): Boolean {
        return posts.remove(id) != null
    }

    override fun getById(id: Int): Post? = posts[id]

    override fun getAll(): List<Post> = posts.values.toList()

    fun reset() {
        posts.clear()
        nextId = 1
    }
}

class PostServiceTest {

    private lateinit var mockPostRepository: MockPostRepository
    private lateinit var postService: PostService

    @BeforeEach
    fun setup() {
        mockPostRepository = MockPostRepository()
        postService = PostService(mockPostRepository)
    }

    @Test
    fun `should create post successfully`() {
        // Arrange
        val request = CreatePostRequest(
            title = "Test Post",
            content = "Test content"
        )
        val principal = UserPrincipal(1, "test@example.com", "USER")

        // Act
        val result = postService.createPost(request, principal)

        // Assert
        assertTrue(result.isSuccess)
        val post = result.getOrNull()
        assertNotNull(post)
        assertEquals("Test Post", post?.title)
        assertEquals(1, post?.authorId)
    }

    @Test
    fun `should allow owner to update post`() {
        // Arrange
        val principal = UserPrincipal(1, "test@example.com", "USER")
        val createRequest = CreatePostRequest("Original", "Content")
        val postId = postService.createPost(createRequest, principal).getOrNull()?.id!!

        // Act
        val updateRequest = UpdatePostRequest("Updated", "New content")
        val result = postService.updatePost(postId, updateRequest, principal)

        // Assert
        assertTrue(result.isSuccess)
        assertEquals("Updated", result.getOrNull()?.title)
    }

    @Test
    fun `should deny non-owner from updating post`() {
        // Arrange
        val owner = UserPrincipal(1, "owner@example.com", "USER")
        val attacker = UserPrincipal(2, "attacker@example.com", "USER")

        val createRequest = CreatePostRequest("Owner's Post", "Content")
        val postId = postService.createPost(createRequest, owner).getOrNull()?.id!!

        // Act
        val updateRequest = UpdatePostRequest("Hacked", "Bad content")
        val result = postService.updatePost(postId, updateRequest, attacker)

        // Assert
        assertTrue(result.isFailure)
        val exception = result.exceptionOrNull()
        assertTrue(exception is ForbiddenException)
    }

    @Test
    fun `should allow admin to update any post`() {
        // Arrange
        val user = UserPrincipal(1, "user@example.com", "USER")
        val admin = UserPrincipal(2, "admin@example.com", "ADMIN")

        val createRequest = CreatePostRequest("User's Post", "Content")
        val postId = postService.createPost(createRequest, user).getOrNull()?.id!!

        // Act
        val updateRequest = UpdatePostRequest("Admin Edit", "Updated by admin")
        val result = postService.updatePost(postId, updateRequest, admin)

        // Assert
        assertTrue(result.isSuccess)
        assertEquals("Admin Edit", result.getOrNull()?.title)
    }

    @Test
    fun `should delete post when owner requests`() {
        // Arrange
        val principal = UserPrincipal(1, "test@example.com", "USER")
        val createRequest = CreatePostRequest("Delete Me", "Content")
        val postId = postService.createPost(createRequest, principal).getOrNull()?.id!!

        // Act
        val result = postService.deletePost(postId, principal)

        // Assert
        assertTrue(result.isSuccess)

        // Verify post is gone
        val getResult = postService.getPostById(postId)
        assertTrue(getResult.isFailure)
    }
}

// src/test/kotlin/com/example/routes/PostRoutesTest.kt
package com.example.routes

import com.example.database.DatabaseFactory
import com.example.models.*
import com.example.module
import io.ktor.client.call.*
import io.ktor.client.plugins.contentnegotiation.*
import io.ktor.client.request.*
import io.ktor.http.*
import io.ktor.serialization.kotlinx.json.*
import io.ktor.server.testing.*
import kotlinx.serialization.json.Json
import org.junit.jupiter.api.*
import org.koin.core.context.stopKoin
import kotlin.test.assertEquals
import kotlin.test.assertNotNull
import kotlin.test.assertTrue

@TestInstance(TestInstance.Lifecycle.PER_CLASS)
class PostRoutesTest {

    @BeforeAll
    fun setup() {
        DatabaseFactory.init()
    }

    @AfterAll
    fun teardown() {
        stopKoin()
    }

    private suspend fun ApplicationTestBuilder.getToken(
        client: io.ktor.client.HttpClient,
        email: String,
        password: String = "SecurePass123!"
    ): String {
        // Register
        client.post("/api/auth/register") {
            contentType(ContentType.Application.Json)
            setBody(RegisterRequest(email, password, email.substringBefore("@")))
        }

        // Login
        val loginResponse = client.post("/api/auth/login") {
            contentType(ContentType.Application.Json)
            setBody(LoginRequest(email, password))
        }

        return loginResponse.body<ApiResponse<LoginResponse>>().data?.token!!
    }

    @Test
    fun `test create post`() = testApplication {
        application { module() }

        val client = createClient {
            install(ContentNegotiation) {
                json(Json { ignoreUnknownKeys = true })
            }
        }

        val token = getToken(client, "post-creator@example.com")

        // Create post
        val response = client.post("/api/posts") {
            header(HttpHeaders.Authorization, "Bearer $token")
            contentType(ContentType.Application.Json)
            setBody(CreatePostRequest("My Post", "Post content"))
        }

        assertEquals(HttpStatusCode.Created, response.status)

        val apiResponse = response.body<ApiResponse<Post>>()
        assertTrue(apiResponse.success)
        assertEquals("My Post", apiResponse.data?.title)
    }

    @Test
    fun `test user cannot update others post`() = testApplication {
        application { module() }

        val client = createClient {
            install(ContentNegotiation) {
                json(Json { ignoreUnknownKeys = true })
            }
        }

        // User A creates post
        val tokenA = getToken(client, "usera@example.com")
        val createResponse = client.post("/api/posts") {
            header(HttpHeaders.Authorization, "Bearer $tokenA")
            contentType(ContentType.Application.Json)
            setBody(CreatePostRequest("User A Post", "Content"))
        }
        val postId = createResponse.body<ApiResponse<Post>>().data?.id!!

        // User B tries to update
        val tokenB = getToken(client, "userb@example.com")
        val updateResponse = client.put("/api/posts/$postId") {
            header(HttpHeaders.Authorization, "Bearer $tokenB")
            contentType(ContentType.Application.Json)
            setBody(UpdatePostRequest("Hacked", "Bad content"))
        }

        assertEquals(HttpStatusCode.Forbidden, updateResponse.status)
    }
}
```
