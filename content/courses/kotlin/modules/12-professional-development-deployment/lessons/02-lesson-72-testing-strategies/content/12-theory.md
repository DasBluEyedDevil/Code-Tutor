---
type: "THEORY"
title: "Exercise 1: Build a Tested Repository"
---


Create a `ProductRepository` with full test coverage.

### Requirements

1. **ProductRepository** with methods:
   - `getProducts(): List<Product>`
   - `getProduct(id: String): Product?`
   - `createProduct(product: Product): Result<Product>`
   - `updateProduct(id: String, product: Product): Result<Product>`
   - `deleteProduct(id: String): Result<Unit>`

2. **Tests** (use MockK):
   - Test successful operations
   - Test error cases (not found, network errors)
   - Test caching behavior
   - Verify mock interactions

---

