---
type: "THEORY"
title: "🎯 Exercise: Product Catalog with Variants"
---


Create a product catalog API with these requirements:

### Requirements

1. **Product** model with:
   - Basic info (id, name, description)
   - Price (use Double)
   - Category (enum: ELECTRONICS, CLOTHING, BOOKS, FOOD)
   - Created/updated timestamps (use LocalDateTime)
   - Variants (list of ProductVariant)

2. **ProductVariant** model with:
   - SKU (stock keeping unit)
   - Size or other attribute
   - Stock quantity
   - Price override (nullable)

3. **Create endpoint** to add products with variants
4. **Handle errors** for invalid JSON
5. **Custom serializer** for timestamps

### Starter Code


---



```kotlin
enum class ProductCategory {
    ELECTRONICS,
    CLOTHING,
    BOOKS,
    FOOD
}

// TODO: Add @Serializable and implement models
data class ProductVariant(
    val sku: String,
    val attribute: String,  // e.g., "Size: Large", "Color: Red"
    val stockQuantity: Int,
    val priceOverride: Double? = null
)

data class Product(
    val id: Int,
    val name: String,
    val description: String,
    val basePrice: Double,
    val category: ProductCategory,
    val variants: List<ProductVariant>,
    val createdAt: LocalDateTime,
    val updatedAt: LocalDateTime
)

// TODO: Create request model
// TODO: Implement routes
```
