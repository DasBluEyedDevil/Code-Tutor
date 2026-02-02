---
type: "THEORY"
title: "Testing Middleware"
---

Middleware in Dart Frog wraps your route handlers to add cross-cutting concerns like authentication, logging, and CORS. Testing middleware ensures these concerns work correctly.

Middleware has a different signature than route handlers - it receives the handler and returns a new handler. This requires a slightly different testing approach.