---
type: "WARNING"
title: "Common Pitfalls"
---

## Product Catalog API Pitfalls

**Not Validating Input on the Server**: Client-side validation provides a good user experience but offers zero security. An attacker can bypass the UI entirely and send malformed requests directly to your API. Always validate with FluentValidation or Data Annotations on the server side, even when the client also validates.

**Returning Domain Entities from API Endpoints**: Exposing your Product entity directly in API responses leaks internal structure (navigation properties, private fields, database IDs) and couples your API contract to your database schema. Map entities to DTOs before returning them. A schema change should not break API consumers.

**Missing Pagination on List Endpoints**: Returning all 10,000 products from `GET /api/products` without pagination creates slow responses, high memory usage, and poor user experience. Always implement pagination with `page` and `pageSize` query parameters, and return total count for UI pagination controls.

**Forgetting CancellationToken**: Every async handler and repository method should accept and forward `CancellationToken`. Without it, a user who navigates away or cancels a request still triggers the full database query and processing. Pass the token from the controller through every async call in the chain.
