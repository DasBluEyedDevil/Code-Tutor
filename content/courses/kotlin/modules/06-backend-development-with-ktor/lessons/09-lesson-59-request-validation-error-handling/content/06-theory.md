---
type: "THEORY"
title: "Exercise: Product Validation System"
---


Build a complete validation system for a product catalog API.

### Requirements

1. **Product Model**:
   - Name (required, 1-200 chars)
   - Description (optional, max 1000 chars)
   - Price (required, must be > 0, max 2 decimal places)
   - Category (required, must be one of: Electronics, Clothing, Books, Food, Other)
   - SKU (required, unique, format: 3 letters + 6 digits, e.g., "ABC123456")
   - Stock quantity (required, must be >= 0)
   - Active (boolean, defaults to true)

2. **Validation Rules**:
   - Price must be positive and not exceed 1,000,000
   - Category must match allowed values exactly (case-sensitive)
   - SKU must be unique across all products
   - Cannot set stock to negative
   - Cannot update inactive products (business rule)

3. **Error Handling**:
   - Return 400 for validation errors with field-specific messages
   - Return 404 when product doesn't exist
   - Return 409 for duplicate SKU
   - Return 422 for business rule violations (updating inactive product)

### Your Task

Implement:
1. `Product` and `CreateProductRequest` data classes
2. `ProductValidator` with all validation rules
3. `ProductService` with create, update, and deactivate methods
4. Custom exception for business rule violations (`BusinessRuleException`)
5. Error handling configuration
6. Routes with proper error responses

Test with these cases:
- Valid product creation
- Missing required fields
- Invalid price (negative, too many decimals)
- Invalid category
- Invalid SKU format
- Duplicate SKU
- Updating inactive product

### Starter Code


---



```kotlin
// Models
@Serializable
data class Product(
    val id: Int,
    val name: String,
    val description: String?,
    val price: Double,
    val category: String,
    val sku: String,
    val stockQuantity: Int,
    val active: Boolean = true
)

@Serializable
data class CreateProductRequest(
    val name: String,
    val description: String? = null,
    val price: Double,
    val category: String,
    val sku: String,
    val stockQuantity: Int
)

@Serializable
data class UpdateProductRequest(
    val name: String,
    val description: String? = null,
    val price: Double,
    val category: String,
    val stockQuantity: Int
)

// TODO: Implement ProductValidator
// TODO: Implement ProductService
// TODO: Implement routes
```
