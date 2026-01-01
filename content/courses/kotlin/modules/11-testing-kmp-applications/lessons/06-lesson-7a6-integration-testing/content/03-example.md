---
type: "EXAMPLE"
title: "Testing API Clients with MockEngine"
---

Ktor provides MockEngine for testing HTTP clients without a real server:

```kotlin
class NotesApiClientTest {
    
    private fun createMockEngine(handler: MockRequestHandler): HttpClient {
        return HttpClient(MockEngine) {
            install(ContentNegotiation) {
                json()
            }
            engine {
                addHandler(handler)
            }
        }
    }
    
    @Test
    fun `fetchNotes returns parsed notes`() = runTest {
        val mockNotes = listOf(
            NoteDto(1, "Note 1", "Content 1", 1000),
            NoteDto(2, "Note 2", "Content 2", 2000)
        )
        
        val client = createMockEngine { request ->
            when (request.url.encodedPath) {
                "/api/notes" -> respond(
                    content = Json.encodeToString(mockNotes),
                    status = HttpStatusCode.OK,
                    headers = headersOf("Content-Type", "application/json")
                )
                else -> error("Unhandled: ${request.url}")
            }
        }
        
        val apiClient = NotesApiClient(client, "https://api.example.com")
        val result = apiClient.fetchNotes()
        
        assertTrue(result.isSuccess)
        assertEquals(2, result.getOrNull()?.size)
    }
    
    @Test
    fun `fetchNotes handles server error`() = runTest {
        val client = createMockEngine { request ->
            respond(
                content = "Internal Server Error",
                status = HttpStatusCode.InternalServerError
            )
        }
        
        val apiClient = NotesApiClient(client, "https://api.example.com")
        val result = apiClient.fetchNotes()
        
        assertTrue(result.isFailure)
    }
    
    @Test
    fun `createNote sends correct payload`() = runTest {
        var capturedBody: String? = null
        
        val client = createMockEngine { request ->
            capturedBody = request.body.toByteArray().decodeToString()
            respond(
                content = Json.encodeToString(NoteDto(1, "Title", "Content", 0)),
                status = HttpStatusCode.Created,
                headers = headersOf("Content-Type", "application/json")
            )
        }
        
        val apiClient = NotesApiClient(client, "https://api.example.com")
        apiClient.createNote("Title", "Content")
        
        assertNotNull(capturedBody)
        assertTrue(capturedBody!!.contains("Title"))
        assertTrue(capturedBody!!.contains("Content"))
    }
}

// Production API client
class NotesApiClient(
    private val httpClient: HttpClient,
    private val baseUrl: String
) {
    suspend fun fetchNotes(): Result<List<NoteDto>> = runCatching {
        httpClient.get("$baseUrl/api/notes").body()
    }
    
    suspend fun createNote(title: String, content: String): Result<NoteDto> = runCatching {
        httpClient.post("$baseUrl/api/notes") {
            contentType(ContentType.Application.Json)
            setBody(CreateNoteRequest(title, content))
        }.body()
    }
}
```
