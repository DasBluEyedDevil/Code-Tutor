---
type: "KEY_POINT"
title: "Product Catalog API Design"
---

## Key Takeaways

- **Apply everything from previous modules** -- the catalog API uses minimal APIs (M11), EF Core (M12), validation, typed results, and OpenAPI documentation (M19). This is where isolated concepts become a real system.

- **Design endpoints around resources, not actions** -- `/api/products` with GET/POST and `/api/products/{id}` with GET/PUT/DELETE. RESTful design makes your API intuitive for any consumer.

- **Include pagination from the start** -- `/api/products?page=1&pageSize=20` prevents performance problems. Loading thousands of products in a single response will eventually crash or timeout.
