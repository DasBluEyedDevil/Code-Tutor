---
type: "KEY_POINT"
title: "Scalar API Documentation"
---

## Key Takeaways

- **`MapScalarApiReference()` replaces Swagger UI** -- Scalar provides a modern, beautiful documentation interface with dark mode, code examples in multiple languages, and built-in API testing.

- **Customize with themes and settings** -- `.WithTheme()`, `.WithDarkMode()`, `.WithDefaultHttpClient()` configure the appearance. `.WithPreferredScheme("Bearer")` hints at your authentication method.

- **Documentation is auto-generated from your code** -- endpoint metadata (`.WithDescription()`, `.WithSummary()`, typed results) flows directly into the Scalar UI. Write good endpoint metadata and documentation writes itself.
