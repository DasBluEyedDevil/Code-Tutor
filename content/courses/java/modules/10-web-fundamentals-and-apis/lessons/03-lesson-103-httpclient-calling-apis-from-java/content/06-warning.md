---
type: "WARNING"
title: "HttpClient Production Considerations"
---

WHEN USING HTTPCLIENT IN PRODUCTION:

1. CONNECTION POOLING
   - Reuse HttpClient instances (they are thread-safe)
   - Creating new clients per request wastes resources
   - Configure connection timeouts appropriately

2. TIMEOUT CONFIGURATION
   HttpClient client = HttpClient.newBuilder()
       .connectTimeout(Duration.ofSeconds(10))
       .build();

3. ERROR HANDLING
   - Handle IOException for network errors
   - Handle InterruptedException for cancellation
   - Check status codes (4xx, 5xx)

4. HTTPS ONLY
   - Never use HTTP for sensitive data
   - Verify SSL certificates in production

5. ASYNC FOR PERFORMANCE
   - Use sendAsync() for non-blocking calls
   - CompletableFuture for concurrent requests
   - Consider reactive alternatives (WebClient)

6. JSON LIBRARIES
   - Gson is simple but Jackson is more powerful
   - Consider using records for DTOs (Java 17+)

7. VIRTUAL THREADS (Java 21+ / Spring Boot 3.4+)
   - HttpClient uses virtual threads when enabled
   - Set spring.threads.virtual.enabled=true
   - Dramatically improves scalability for I/O-heavy apps
   - No code changes needed - same blocking code, better performance