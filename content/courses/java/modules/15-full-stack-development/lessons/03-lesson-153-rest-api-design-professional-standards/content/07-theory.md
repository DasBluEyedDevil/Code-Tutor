---
type: "THEORY"
title: "Pagination for Large Collections"
---

DON'T return thousands of items at once!

BAD:
GET /api/products
Returns: 50,000 products (crashes browser)

GOOD:
GET /api/products?page=0&size=20
Returns: First 20 products + metadata

Response format:
{
  "content": [
    {"id": 1, "name": "Product 1"},
    {"id": 2, "name": "Product 2"}
  ],
  "page": 0,
  "size": 20,
  "totalElements": 500,
  "totalPages": 25
}

Spring Data JPA makes this easy:

@GetMapping("/api/products")
public Page<Product> getAll(
        @RequestParam(defaultValue = "0") int page,
        @RequestParam(defaultValue = "20") int size) {
    Pageable pageable = PageRequest.of(page, size);
    return productRepository.findAll(pageable);
}

FILTERING:
GET /api/products?category=electronics&minPrice=100

@GetMapping("/api/products")
public List<Product> search(
        @RequestParam(required = false) String category,
        @RequestParam(required = false) Double minPrice) {
    return productService.search(category, minPrice);
}