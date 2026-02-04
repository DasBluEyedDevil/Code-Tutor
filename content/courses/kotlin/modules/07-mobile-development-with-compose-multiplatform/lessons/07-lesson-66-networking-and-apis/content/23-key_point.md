---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Ktor Client provides multiplatform HTTP networking** with platform-specific engines (OkHttp for Android, NSURLSession for iOS, curl for Native). Write network code once in commonMain and deploy everywhere.

**Use content negotiation with kotlinx-serialization** for automatic JSON parsing. Install `ContentNegotiation` plugin with `json()` to serialize/deserialize data classes transparently.

**Always handle network errors and loading states** in your UI. Network calls can fail (no connection, timeouts, server errors)—wrap calls in try-catch and provide user feedback for all states (loading, success, error).
