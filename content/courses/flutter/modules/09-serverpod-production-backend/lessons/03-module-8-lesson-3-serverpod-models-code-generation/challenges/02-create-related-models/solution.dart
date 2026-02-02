# protocol/category.yaml
class: Category
table: categories
fields:
  name: String
  description: String?
  isActive: bool, default='true'

---

# protocol/product.yaml
class: Product
table: products
fields:
  name: String
  price: double
  categoryId: int
  category: Category?, relation=categoryId

indexes:
  product_category_idx:
    fields: categoryId