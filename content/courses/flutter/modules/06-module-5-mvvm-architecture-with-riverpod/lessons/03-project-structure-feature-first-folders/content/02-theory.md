---
type: "THEORY"
title: "Feature-First vs Layer-First"
---

There are two main ways to organize a Flutter project. Understanding both helps you choose wisely.

### Layer-First Organization

Organize by the TYPE of file:

```
lib/
  models/
    user_model.dart
    product_model.dart
    cart_model.dart
    order_model.dart
  views/
    login_screen.dart
    product_screen.dart
    cart_screen.dart
    checkout_screen.dart
  viewmodels/
    login_viewmodel.dart
    product_viewmodel.dart
    cart_viewmodel.dart
    checkout_viewmodel.dart
```

**Problem:** Related files are scattered. To work on the cart feature, you need to jump between three different folders. If you have 50 models, finding `cart_model.dart` means scrolling through an alphabetical list of unrelated models.

### Feature-First Organization

Organize by the FEATURE:

```
lib/
  features/
    auth/
      models/user.dart
      viewmodels/login_viewmodel.dart
      views/login_screen.dart
    cart/
      models/cart_item.dart
      viewmodels/cart_viewmodel.dart
      views/cart_screen.dart
    checkout/
      models/order.dart
      viewmodels/checkout_viewmodel.dart
      views/checkout_screen.dart
```

**Benefit:** Everything for the cart is in one place. Need to work on cart? Open the `cart/` folder and everything you need is right there.

### Why Feature-First Wins

1. **Related code together:** Model, ViewModel, and View for a feature are neighbors
2. **Easier to find:** Looking for cart code? Go to `features/cart/`
3. **Easy to delete:** Removing a feature? Delete one folder
4. **Better for teams:** Each developer can own a feature folder
5. **Scales well:** 50 features means 50 folders, not 150 files in one folder