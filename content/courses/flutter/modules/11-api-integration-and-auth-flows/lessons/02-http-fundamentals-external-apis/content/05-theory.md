---
type: "THEORY"
title: "Interceptors: The Power of Middleware"
---


Interceptors are one of Dio's most powerful features. They allow you to intercept requests and responses at various stages, enabling:

- **Logging**: See every request and response for debugging
- **Authentication**: Automatically add auth headers to every request
- **Retry Logic**: Automatically retry failed requests
- **Token Refresh**: Refresh expired tokens and retry the request
- **Caching**: Cache responses to reduce network calls
- **Error Transformation**: Convert API errors to your app's error types

**The Interceptor Lifecycle:**

```
Request Created
     |
     v
+----------------+
| onRequest      |  <- Modify request before sending
+----------------+
     |
     v
  [Network]
     |
     v
+----------------+
| onResponse     |  <- Process successful response
+----------------+
     |
     v
Success Handler

     OR (if error)

+----------------+
| onError        |  <- Handle errors, retry, transform
+----------------+
     |
     v
Error Handler
```

**Adding Multiple Interceptors:**

Interceptors execute in the order they are added. A common pattern:

1. Logging interceptor (see all traffic)
2. Auth interceptor (add tokens)
3. Retry interceptor (handle transient failures)
4. Error interceptor (transform errors)

Let us implement each of these.

