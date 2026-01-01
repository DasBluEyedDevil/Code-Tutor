---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Issues!

**Returning 200 for errors**: Don't return 200 OK for validation failures! Use 400 BadRequest. Status codes communicate meaning to clients!

**Empty BadRequest messages**: 'Results.BadRequest()' with no message is unhelpful! Always include: 'Results.BadRequest("Name is required!")'. Help clients fix issues!

**Using 200 instead of 201 for POST**: Created resources should return 201 Created, not 200 OK! Use 'TypedResults.Created(uri, value)' for proper semantics.

**Returning null instead of 404**: 'return null' gives 204 No Content, not 404! Use 'TypedResults.NotFound()' for missing resources.

**Inconsistent error formats**: Use Problem Details (TypedResults.Problem()) for consistent error responses that follow RFC 7807 standard.