---
type: "THEORY"
title: "Error Handling: DioException Types"
---


Dio provides detailed exception types that help you understand exactly what went wrong. Proper error handling is essential for a good user experience.

**DioExceptionType Enumeration:**

| Type | When It Occurs | User Message Suggestion |
|------|----------------|------------------------|
| `connectionTimeout` | Could not connect to server within timeout | "Connection timed out. Please check your internet." |
| `sendTimeout` | Request body took too long to send | "Upload timed out. Please try with a smaller file." |
| `receiveTimeout` | Response took too long to arrive | "Server is slow to respond. Please try again." |
| `badCertificate` | SSL certificate issue | "Security error. Please update the app." |
| `badResponse` | Server returned error status (4xx, 5xx) | Depends on status code |
| `cancel` | Request was cancelled (by code) | "Request was cancelled." |
| `connectionError` | No internet, DNS failure, etc. | "No internet connection." |
| `unknown` | Any other error | "An unexpected error occurred." |

**HTTP Status Codes to Handle:**

| Code | Meaning | Typical Response |
|------|---------|------------------|
| 200 | OK | Success |
| 201 | Created | Resource created successfully |
| 204 | No Content | Success, no response body |
| 400 | Bad Request | Show validation errors |
| 401 | Unauthorized | Redirect to login |
| 403 | Forbidden | Show "no permission" message |
| 404 | Not Found | Show "not found" message |
| 409 | Conflict | Resource already exists |
| 422 | Unprocessable Entity | Show validation errors |
| 429 | Too Many Requests | Show "slow down" message, retry later |
| 500 | Internal Server Error | Show "server error" message |
| 502 | Bad Gateway | Show "server error" message |
| 503 | Service Unavailable | Show "temporarily unavailable" message |

**Best Practices:**

1. **Never expose raw error messages** - Server errors might contain sensitive info
2. **Provide actionable messages** - Tell users what to do, not just what happened
3. **Log detailed errors** - For debugging, but not in production builds
4. **Retry transient errors** - Network and 5xx errors often resolve themselves
5. **Handle offline gracefully** - Check connectivity before showing errors

