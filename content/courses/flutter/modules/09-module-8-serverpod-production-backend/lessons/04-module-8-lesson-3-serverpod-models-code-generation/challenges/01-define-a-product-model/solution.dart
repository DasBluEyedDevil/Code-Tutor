# protocol/product.yaml
# Product model for e-commerce application

class: Product
table: products
fields:
  name: String
  # Required product name

  description: String?
  # Optional product description

  price: double
  # Required price in dollars

  stockQuantity: int, default='0'
  # How many items in stock, defaults to 0

  isAvailable: bool, default='true'
  # Whether product can be purchased

  createdAt: DateTime
  # When the product was added

  categoryId: int
  # Foreign key to categories table

  imageUrls: List<String>?
  # Optional list of image URLs

indexes:
  product_category_idx:
    fields: categoryId
  product_available_idx:
    fields: isAvailable