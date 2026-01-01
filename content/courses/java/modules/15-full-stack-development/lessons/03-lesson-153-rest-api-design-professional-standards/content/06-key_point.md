---
type: "KEY_POINT"
title: "Consistent Response Structure"
---

Always return JSON in a predictable format:

SUCCESS Response:
{
  "data": {
    "id": 123,
    "name": "Product Name",
    "price": 29.99
  },
  "timestamp": "2025-01-15T10:30:00Z"
}

ERROR Response:
{
  "error": {
    "code": 404,
    "message": "Product not found",
    "details": "No product exists with ID 123"
  },
  "timestamp": "2025-01-15T10:30:00Z",
  "path": "/api/products/123"
}

Spring Boot Implementation:

public record ApiResponse<T>(T data, LocalDateTime timestamp) {
    public ApiResponse(T data) {
        this(data, LocalDateTime.now());
    }
}

@GetMapping("/api/products/{id}")
public ResponseEntity<ApiResponse<Product>> getProduct(@PathVariable Long id) {
    Product product = productService.findById(id);
    return ResponseEntity.ok(new ApiResponse<>(product));
}