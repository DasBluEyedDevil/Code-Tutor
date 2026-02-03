---
type: "KEY_POINT"
title: "RESTful CRUD Routing"
---

## Key Takeaways

- **Match HTTP methods to operations** -- GET reads, POST creates, PUT updates, DELETE removes. Following REST conventions makes your API predictable for any client developer.

- **Route parameters use `{name}` syntax** -- `/api/products/{id}` captures the ID from the URL. The handler parameter `(int id)` receives the value automatically.

- **POST returns `TypedResults.Created()`** -- return 201 Created with the URL of the new resource and the created object. This is the standard REST pattern for resource creation.
