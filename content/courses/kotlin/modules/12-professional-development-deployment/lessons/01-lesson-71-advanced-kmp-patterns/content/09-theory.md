---
type: "THEORY"
title: "Sharing Business Logic"
---


### Example: Shopping Cart

**Shared Models (commonMain)**:

**Shared Business Logic (commonMain)**:

**Usage in Android**:

**Usage in iOS**:

---



```swift
// iOS ViewModel (Swift)
class ShoppingViewModel: ObservableObject {
    private let cart = ShoppingCart() // Same shared code!

    @Published var items: [CartItem] = []

    func addToCart(product: Product) {
        cart.addItem(product: product, quantity: 1)
        items = cart.getItems()
    }

    var total: Double {
        cart.getTotal()
    }
}
```
