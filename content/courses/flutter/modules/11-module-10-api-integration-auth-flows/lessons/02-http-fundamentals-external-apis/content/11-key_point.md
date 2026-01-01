---
type: "KEY_POINT"
title: "Lesson Summary"
---


You have learned how to communicate with external APIs from Flutter. Here are the key takeaways:

**When to Use Dio:**
- Calling third-party APIs (weather, payments, social media)
- Services that do not have a Serverpod-style generated client
- When you need interceptors, retry logic, or detailed control

**Dio Setup:**
- Create configured Dio instances with BaseOptions
- Set baseUrl, timeouts, and default headers
- Use factory methods for different APIs

**HTTP Methods:**
- GET: Fetch data (idempotent, cacheable)
- POST: Create resources (include body)
- PUT: Replace entire resource
- PATCH: Partial update
- DELETE: Remove resource

**Interceptors:**
- LoggingInterceptor: Debug all HTTP traffic
- AuthInterceptor: Add authentication headers
- RetryInterceptor: Retry transient failures
- ErrorInterceptor: Transform errors to app types

**Error Handling:**
- DioException provides detailed error types
- Transform to user-friendly messages
- Retry network and server errors
- Handle 401 by redirecting to login

**Best Practices:**
- Never hardcode API keys
- Cache responses to reduce API calls
- Respect rate limits
- Use your backend for sensitive operations
- Combine Serverpod client and Dio as needed

In the next lesson, you will learn about advanced authentication flows including OAuth, social login, and token refresh patterns.

