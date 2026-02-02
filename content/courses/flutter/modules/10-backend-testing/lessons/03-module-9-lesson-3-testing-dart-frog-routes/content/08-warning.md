---
type: "WARNING"
title: "Common Mocking Mistakes"
---

Avoid these common pitfalls when mocking RequestContext:

1. **Forgetting to mock nested objects**: If your handler calls context.request.uri.queryParameters, you must mock the entire chain

2. **Not handling async body methods**: Request.body() and request.json() are async - use thenAnswer, not thenReturn

3. **Ignoring HTTP method checks**: If your handler behaves differently for GET vs POST, test both paths

4. **Missing dependency mocks**: If your handler uses context.read<SomeService>(), you must mock that service

5. **Not resetting mocks between tests**: Use setUp() to create fresh mocks for each test