---
type: "KEY_POINT"
title: "API Versioning Strategies"
---

## Key Takeaways

- **URL segment versioning is the clearest** -- `/api/v1/products` makes the version visible and bookmarkable. Combine with header versioning via `ApiVersionReader.Combine()` for flexibility.

- **Set `AssumeDefaultVersionWhenUnspecified = true`** -- this ensures existing clients without version parameters continue working when you add versioning. Backward compatibility is critical.

- **Major version = breaking changes, minor = additions** -- `ApiVersion(2, 0)` means the contract changed. `ApiVersion(1, 1)` means new endpoints were added without breaking existing ones.
