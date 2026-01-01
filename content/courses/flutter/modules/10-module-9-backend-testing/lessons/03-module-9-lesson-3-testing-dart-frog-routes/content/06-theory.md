---
type: "THEORY"
title: "Mocking RequestContext"
---

The RequestContext is the heart of every Dart Frog handler. It provides access to:

- The HTTP request (method, headers, body, URI)
- Route parameters
- Query parameters
- Dependency injection (via context.read)
- Request/response lifecycle

To test handlers in isolation, you must mock the RequestContext and configure it to return the data your handler expects.