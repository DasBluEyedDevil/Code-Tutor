---
type: "THEORY"
title: "💻 Complete REST Controller with Best Practices"
---

```java
@RestController
@RequestMapping("/api/v1/products")
@Validated
public class ProductController {
    private final ProductService productService;
    
    public ProductController(ProductService productService) {
        this.productService = productService;
    }
    
    // GET all (with pagination)
    @GetMapping
    public ResponseEntity<Page<Product>> getAll(
            @RequestParam(defaultValue = "0") int page,
            @RequestParam(defaultValue = "20") int size) {
        Pageable pageable = PageRequest.of(page, size);
        Page<Product> products = productService.findAll(pageable);
        return ResponseEntity.ok(products);  // 200 OK
    }
    
    // GET one
    @GetMapping("/{id}")
    public ResponseEntity<Product> getById(@PathVariable Long id) {
        return productService.findById(id)
            .map(ResponseEntity::ok)  // 200 OK
            .orElse(ResponseEntity.notFound().build());  // 404 Not Found
    }
    
    // POST (create)
    @PostMapping
    public ResponseEntity<Product> create(
            @Valid @RequestBody Product product) {
        Product saved = productService.save(product);
        URI location = URI.create("/api/v1/products/" + saved.getId());
        return ResponseEntity.created(location).body(saved);  // 201 Created
    }
    
    // PUT (replace)
    @PutMapping("/{id}")
    public ResponseEntity<Product> replace(
            @PathVariable Long id,
            @Valid @RequestBody Product product) {
        if (!productService.exists(id)) {
            return ResponseEntity.notFound().build();  // 404
        }
        Product updated = productService.replace(id, product);
        return ResponseEntity.ok(updated);  // 200 OK
    }
    
    // PATCH (partial update)
    @PatchMapping("/{id}")
    public ResponseEntity<Product> update(
            @PathVariable Long id,
            @RequestBody Map<String, Object> updates) {
        return productService.partialUpdate(id, updates)
            .map(ResponseEntity::ok)  // 200 OK
            .orElse(ResponseEntity.notFound().build());  // 404
    }
    
    // DELETE
    @DeleteMapping("/{id}")
    public ResponseEntity<Void> delete(@PathVariable Long id) {
        if (!productService.exists(id)) {
            return ResponseEntity.notFound().build();  // 404
        }
        productService.delete(id);
        return ResponseEntity.noContent().build();  // 204 No Content
    }
}

This controller demonstrates:
✓ Correct HTTP methods
✓ Proper status codes
✓ Resource-based URLs (no verbs)
✓ Pagination support
✓ Validation with @Valid
✓ Constructor injection
✓ API versioning in path
```