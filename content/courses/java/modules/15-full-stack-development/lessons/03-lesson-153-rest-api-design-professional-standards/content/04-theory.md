---
type: "THEORY"
title: "The 5 Essential HTTP Methods"
---

GET - Retrieve data:
@GetMapping("/api/products")
public List<Product> getAll() { }

@GetMapping("/api/products/{id}")
public Product getById(@PathVariable Long id) { }

POST - Create new resource:
@PostMapping("/api/products")
public ResponseEntity<Product> create(@RequestBody Product product) {
    Product saved = productService.save(product);
    return ResponseEntity
        .created(URI.create("/api/products/" + saved.getId()))
        .body(saved);
}

PUT - Replace entire resource:
@PutMapping("/api/products/{id}")
public Product replace(@PathVariable Long id, @RequestBody Product product) {
    return productService.replace(id, product);
}

PATCH - Partial update:
@PatchMapping("/api/products/{id}")
public Product update(@PathVariable Long id, @RequestBody Map<String, Object> updates) {
    return productService.partialUpdate(id, updates);
}

DELETE - Remove resource:
@DeleteMapping("/api/products/{id}")
public ResponseEntity<Void> delete(@PathVariable Long id) {
    productService.delete(id);
    return ResponseEntity.noContent().build();
}