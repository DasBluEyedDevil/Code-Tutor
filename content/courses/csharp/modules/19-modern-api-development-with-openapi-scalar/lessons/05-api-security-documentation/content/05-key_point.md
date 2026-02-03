---
type: "KEY_POINT"
title: "Documenting API Security"
---

## Key Takeaways

- **Define security schemes in the OpenAPI document** -- `SecuritySchemeType.Http` with `BearerFormat = "JWT"` tells consumers exactly how to authenticate. This metadata drives the "Authorize" button in documentation UIs.

- **Apply security globally or per-endpoint** -- `document.SecurityRequirements` applies to all endpoints. Individual endpoints can override with their own requirements or opt out of security.

- **Security documentation is part of the API contract** -- consumers should be able to authenticate just by reading the OpenAPI spec. Include clear descriptions of token acquisition, scopes, and error responses.
